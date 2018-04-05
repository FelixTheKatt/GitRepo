using Agenda.Resources.Styles;
using Agenda.ViewModel.AgendaFolder;
using Agenda.ViewModel.SessionFolder;
using Agenda.Windows.Group;
using Dal.Model;
using Dal.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ToolboxMvvm.wpf;

namespace Agenda.ViewModel.GroupFolder
{
    public class GroupManagerDataContext : ViewModelBase
    {
        private Group group;

        public Group Group
        {
            get { return group; }
            set { group = value; }
        }


        public string Name
        {
            get
            {
                return Group.Name;
            }
        }
        public SolidColorBrush GroupColor
        {
            get
            {
                return new SolidColorBrush(CustomColors.FromString(Group.RGBAColor));
            }
        }

        public int TotalEvents
        {
            get
            {

                return GetEventgroup().Count();
            }
        }

        public int EventThisYEar
        {
            get
            {
                return GetEventgroup().Where(x => x.Date.Year >= DateTime.Now.Year).ToList().Count();
            }
        }

        private List<Event> GetEventgroup()
        {
            return EventRepo.Instance.GetAll().Where(x => x.GroupId == group.GroupID).ToList();
        }

        public GroupManagerDataContext DataContext
        {
            get
            {
                return this;
            }
        }

        public GroupManagerViewModel GMVM { get ;  set; }

        public GroupManagerDataContext()
        {

        }
        public GroupManagerDataContext(Group group)
        {
            this.Group = group;
        }
        private ICommand deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand = deleteCommand ?? new CommandBase(Delete);
            }
        }

        public void Delete()
        {
            if (Group.Name == "DefaultGroup")
            {
                MessageBox.Show($"You can't delete {Group.Name} !!!!", "NO", MessageBoxButton.OK);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the event {Group.Name} ?", "Warning", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    List<ColorEventDataContext> list = AgendaViewModelCollection.Instance.EventList.Where(x => x.EventView.GroupId == Group.GroupID).ToList();


                    if (list.FirstOrDefault() == null)
                    {
                        GroupRepo.Instance.Delete(Group.GroupID);

                        ReassignGroups();
                    }

                    else
                    {
                        MessageBoxResult result2 = MessageBox.Show($"do you want to move event from {Group.Name} to another Group ?" +
                            $"no = delete all related event", "Warning", MessageBoxButton.YesNoCancel);

                        if (result2 == MessageBoxResult.No)
                        {
                            list.ForEach(x => EventRepo.Instance.Delete(x.EventView.EventId));
                            GroupRepo.Instance.Delete(Group.GroupID);

                            ReassignGroups();

                        }
                        else if (result2 == MessageBoxResult.Yes)
                        {
                            GroupManagerViewModel gmvm = GMVM;

                            GroupComboBox window = new GroupComboBox
                            {
                                DataContext = gmvm
                            };
                            gmvm.OnRequestClose += (s, e) => window.Close();
                            gmvm.AllGroup.Remove(this);
                            //If doesn't work
                            //Try new GMDT () {allProperties = this.allProperties}
                            gmvm.OldGroup = this;
                            gmvm.LabelGroupComboBox(this);

                            window.ShowDialog();
                        }

                    }

                    AgendaViewModelCollection.Instance.LoadEvent?.Invoke(this, EventArgs.Empty);

                    GMVM.RefreshGroup();
                }        
            }
        }

        private void ReassignGroups()
        {

            //IT's GOOD
            GMVM.AllGroup.Remove(this);
            //
            GMVM.AllGroup.ForEach(x => x.GMVM = GMVM);
        }
    }
}

