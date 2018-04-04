using Agenda.Resources.Styles;
using Agenda.Utils.Enums;
using Agenda.ViewModel.AgendaFolder;
using Agenda.ViewModel.SessionFolder;
using Dal.Model;
using Dal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using ToolboxMvvm.wpf;

namespace Agenda.ViewModel.GroupFolder
{
    public class GroupViewModel : ViewModelBase
    {

        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    RaisePropertyChanged(nameof(Id));
                }
            }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    CheckName();
                    RaisePropertyChanged(nameof(Name));
                    RaisePropertyChanged(nameof(WarningName));

                }
            }
        }

        private string color;

        public string Color
        {
            get { return color; }
            set
            {
                if (color != value)
                {
                    color = value;
                    RaisePropertyChanged(nameof(Color));
                }

            }
        }
        public string WarningName
        {
            get
            {
                if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
                {
                    return WarningUtils.ErrorCodes.Where(x => x.Key == "Null").FirstOrDefault().Value;
                }

                if (AlreadyExist())
                {
                    return WarningUtils.ErrorCodes.Where(x => x.Key == "AlreadyExist").FirstOrDefault().Value;
                }

                return "";
            }
            
                
        }
        private SolidColorBrush namelabelcolor;

        public SolidColorBrush Namelabelcolor
        {
            get { return namelabelcolor ?? (namelabelcolor = new SolidColorBrush()); }
            set { namelabelcolor = value; RaisePropertyChanged(nameof(Namelabelcolor)); }
        }
        private void CheckName()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
            {
                Namelabelcolor.Color = CustomColors.Warning;
            }
            else if (AlreadyExist())
            {
                Namelabelcolor.Color = CustomColors.Info;
            }
        }

        private List<ColorDataContext> colorList;
        public List<ColorDataContext> ColorList
        {
            get
            {
                if (colorList == null)
                {
                    colorList = new List<ColorDataContext>() ;

                    foreach (KeyValuePair<string, Color> color in ColorDictionnary.Colors)
                    {
                        colorList.Add(new ColorDataContext(color.Key, new SolidColorBrush(color.Value)));
                    }
                }
                return colorList;
            }
        }

        public EventHandler RefreshRepo;

        private bool AlreadyExist ()
        {
            int agendaId = AgendaRepo.Instance.GetAll().Where(x => x.UserId == SessionManager.CurrentUser.UserId).FirstOrDefault().AgendaId;
            List <Group> grouplist= GroupRepo.Instance.GetAll().Where(x => x.AgendaId == agendaId).ToList();

            return grouplist.Where(x => x.Name == Name).FirstOrDefault() != null;


        }
        private ColorDataContext selectedColor;

        public ColorDataContext SelectedColor
        {
            get { return selectedColor; }
            set
            {
                if (selectedColor != value)
                {
                    selectedColor = value;
                    RaisePropertyChanged(nameof(SelectedColor));
                    //RaisePropertyChanged(nameof(WarningGroup));
                }
            }
        }
        private ICommand confirmCommand;

        public ICommand ConfirmCommand
        {
            get
            {
                return confirmCommand = confirmCommand ?? new CommandBase(Confirm,CanConfirm);
            }
        }
        public void Confirm ()
        {

            if (Name != null && selectedColor != null)
            {
                Group newGroup = new Group()
                {
                    Name = Name,
                    RGBAColor = CustomColors.ColorToString(selectedColor.ColorPreset.Color),
                    AgendaId = AgendaRepo.Instance.GetAll().Where(x => x.UserId == SessionManager.CurrentUser.UserId).FirstOrDefault().AgendaId

                };
                newGroup.GroupID = GroupRepo.Instance.Insert(newGroup);

                RefreshRepo?.Invoke(this,EventArgs.Empty);

                AgendaViewModelCollection.Instance.LoadEvent(this,EventArgs.Empty);

                CloseWindow();

            }
        }

        private bool CanConfirm ()
        {
            return (!string.IsNullOrEmpty(Name) && !string.IsNullOrWhiteSpace(Name) && SelectedColor != null  );
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

    }
}
