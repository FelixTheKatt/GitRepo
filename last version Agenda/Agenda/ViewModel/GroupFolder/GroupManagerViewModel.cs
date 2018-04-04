using Agenda.ViewModel.AgendaFolder;
using Agenda.ViewModel.AgendaFolder.Calendar;
using Agenda.ViewModel.SessionFolder;
using Dal.Model;
using Dal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToolboxMvvm.wpf;

namespace Agenda.ViewModel.GroupFolder
{
    public class GroupManagerViewModel : ViewModelBase
    {

        private GroupManagerDataContext oldGroup;

        public GroupManagerDataContext OldGroup
        {
            get { return oldGroup; }
            set
            {
                if (oldGroup != value)
                {
                    oldGroup = value;
                    RaisePropertyChanged(nameof(OldGroup));
                }
            }
        }

        private List<GroupManagerDataContext> allGroup;

        public List<GroupManagerDataContext> AllGroup
        {
            get { return allGroup; }
            set
            {
                allGroup = value;
                RaisePropertyChanged(nameof(AllGroup));
            }
        }
        private string concatenation;

        public string Concatenation
        {
            get { return concatenation; }
            set { concatenation = value; }
        }

        public void LabelGroupComboBox(GroupManagerDataContext gmdc)
        {
            string groupName = gmdc.Name;
            string phrase = $"Group To delete is {groupName} , its events will move to =>";

            Concatenation = phrase;
        }

        private GroupManagerDataContext selectedGroup;

        public GroupManagerDataContext SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                if (selectedGroup != value)
                {
                    selectedGroup = value;
                    RaisePropertyChanged(nameof(SelectedGroup));
                }
            }
        }

        public void RefreshGroup()
        {
            int agendaId = AgendaRepo.Instance.GetAll().Where(x => x.UserId == SessionManager.CurrentUser.UserId).FirstOrDefault().AgendaId;
            List<Group> listGroup = GroupRepo.Instance.GetAll().Where(x => x.AgendaId == agendaId).ToList();

            List<GroupManagerDataContext> gmdcList = new List<GroupManagerDataContext>();

            listGroup.ForEach(x => gmdcList.Add(new GroupManagerDataContext(GroupRepo.Instance.GetOne(x.GroupID))));

            AllGroup = gmdcList;

            AllGroup.ForEach(x => x.GMVM = this);

        }

        private ICommand cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                return cancelCommand = cancelCommand ?? new CommandBase(Cancel);
            }
        }
        private void Cancel()
        {
            CloseWindow();
        }

        private ICommand confirmCommand;

        public ICommand ConfirmCommand
        {
            get
            {
                return confirmCommand = confirmCommand ?? new CommandBase(Confirm);
            }
        }
        private void Confirm()
        {
            List<ColorEventDataContext> oldEvents = AgendaViewModelCollection.Instance.EventList.Where(x => x.EventView.GroupId == OldGroup.Group.GroupID).ToList();

            List<CustomMonth> masterCmList = new List<CustomMonth>();

            foreach (ColorEventDataContext item in oldEvents)
            {
                item.EventView.GroupId = SelectedGroup.Group.GroupID;

                EventRepo.Instance.Update(item.EventView);

                List<CustomMonth> cmList = AgendaViewModelCollection.Instance.CustomMonthList.Where(x => x.Days.Any(y => y.DayInMonth == item.EventView.Date.Day && y.Month == item.EventView.Date.Month)).ToList();
                masterCmList.AddRange(cmList);
            }

            masterCmList = masterCmList.Distinct().ToList();

            GroupRepo.Instance.Delete(OldGroup.Group.GroupID);           

            foreach (CustomMonth cm in masterCmList)
            {
                if (cm != null)
                {
                    cm.Days.ForEach(x => x.UpdateListFromAvm.Invoke(this, EventArgs.Empty));
                }
            }

            CloseWindow();

            CloseWindow();
        }
    }
}
