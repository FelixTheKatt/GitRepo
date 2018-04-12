using Agenda.Resources.Styles;
using Agenda.Utils.Enums;
using Agenda.ViewModel.AgendaFolder;
using Agenda.ViewModel.AgendaFolder.Calendar;
using Agenda.ViewModel.GroupFolder;
using Agenda.ViewModel.SessionFolder;
using Agenda.Windows.Group;
using Dal.Model;
using Dal.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using ToolboxMvvm.wpf;

namespace Agenda.ViewModel.EventFolder
{
    public class EventViewModel : ViewModelBase
    {

        private int eventId;

        public int EventId
        {
            get { return eventId; }
            set { eventId = value; }
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
                    RaisePropertyChanged(nameof(Name));
                    RaisePropertyChanged(nameof(WarningEventName));
                }
            }
        }

        public List<int> YearsList
        {
            get
            {
                List<int> yearsList = new List<int>();

                for (int i = 2000; i < 2100; i++)
                {
                    yearsList.Add(i);
                }

                return yearsList;
            }
        }



        private int dateYear;
        public int DateYear
        {
            get { return dateYear; }
            set
            {
                if (dateYear != value)
                {
                    SetDaysListFromYear(value);

                    dateYear = value;
                    RaisePropertyChanged(nameof(DateYear));
                }
            }

        }
        public List<int> MonthsList
        {
            get
            {
                List<int> monthsList = new List<int>();

                for (int i = 1; i < 13; i++)
                {
                    monthsList.Add(i);
                }

                return monthsList;
            }
        }
        private int dateMonth;
        public int DateMonth
        {
            get { return dateMonth; }
            set
            {
                if (dateMonth != value)
                {
                    SetDaysListFromMonth(value);

                    dateMonth = value;
                    RaisePropertyChanged(nameof(DateMonth));
                    
                }
            }
        }
        private List<int> daysList;

        public List<int> DaysList
        {
            get
            {

                return daysList;
            }
            set
            {
                if (daysList != value)
                {
                    daysList = value;
                    RaisePropertyChanged(nameof(DaysList));
                }
            }
        }
        private int dateDay;
        public int DateDay
        {
            get { return dateDay; }
            set
            {
                if (dateDay != value)
                {
                    dateDay = value;
                    RaisePropertyChanged(nameof(DateDay));
                }
            }
        }
        private int? timeHours;

        public int? TimeHours
        {
            get { return timeHours; }
            set
            {
                if (timeHours != value)
                {
                    timeHours = value;
                    CheckTimeSelected();
                    RaisePropertyChanged(nameof(TimeHours));
                    RaisePropertyChanged(nameof(WarningTime));
                }
            }
        }
        public List<int> HoursList
        {
            get
            {
                List<int> hoursList = new List<int>();

                for (int i = 0; i <= 23; i++)
                {
                    hoursList.Add(i);
                }

                return hoursList;
            }
        }
        private int? timeMinutes;
        public int? TimeMinutes
        {
            get { return timeMinutes; }
            set
            {
                if (timeMinutes != value)
                {
                    timeMinutes = value;
                    CheckTimeSelected();
                    RaisePropertyChanged(nameof(TimeMinutes));
                    RaisePropertyChanged(nameof(WarningTime));
                }
            }
        }
        public List<int> MinutesList
        {
            get
            {
                List<int> minutesList = new List<int>();

                for (int i = 0; i <= 59; i++)
                {
                    minutesList.Add(i);
                }

                return minutesList;
            }
        }


        private bool checkedTime;

        public bool CheckedTime
        {
            get { return checkedTime; }
            set
            {
                if (checkedTime != value)
                {
                    checkedTime = value;
                    CheckTimeSelected();
                    RaisePropertyChanged(nameof(CheckedTime));
                    RaisePropertyChanged(nameof(WarningTime));
                }
            }
        }

        private GroupDataContext selectedGroup;

        public GroupDataContext SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                if (selectedGroup != value)
                {
                    selectedGroup = value;
                    RaisePropertyChanged(nameof(SelectedGroup));
                    RaisePropertyChanged(nameof(WarningGroup));
                }
            }
        }
        private ObservableCollection<GroupDataContext> allGroup;

        public ObservableCollection<GroupDataContext> AllGroup
        {
            get
            {
                if (allGroup == null)
                {
                    LoadRepo();
                }
                return allGroup;

            }
            set { allGroup = value; RaisePropertyChanged(nameof(AllGroup)); }
        }

        public ObservableCollection<GroupDataContext> GetAllGroup()
        {
            int agendaId = AgendaRepo.Instance.GetAll().Where(x => x.UserId == SessionManager.CurrentUser.UserId).FirstOrDefault().AgendaId;
            List<Group> grouplist = GroupRepo.Instance.GetAll().Where(x => x.AgendaId == agendaId).ToList();

            ObservableCollection<GroupDataContext> groupCollection = new ObservableCollection<GroupDataContext>();

            foreach (var group in grouplist)
            {
                groupCollection.Add(new GroupDataContext(group));
            }

            
            return groupCollection;
        }


        public string WarningEventName
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return WarningUtils.ErrorCodes.Where(x => x.Key == "Null").FirstOrDefault().Value;
                }
                return "";
            }
        }
        public string WarningTime
        {
            get
            {
                if (CheckedTime == true && (TimeHours == null || TimeMinutes == null))
                {
                    return WarningUtils.ErrorCodes.Where(x => x.Key == "Null").FirstOrDefault().Value;
                }
                if (!(CheckedTime == true || (TimeHours == null || TimeMinutes == null)))
                {
                    return WarningUtils.ErrorCodes.Where(x => x.Key == "TimeUnchecked").FirstOrDefault().Value;
                }
                return "";
            }
        }
        public string WarningGroup
        {
            get
            {
                if (SelectedGroup == null)
                {
                    return WarningUtils.ErrorCodes.Where(x => x.Key == "Null").FirstOrDefault().Value;
                }
                return "";
            }
        }

        private SolidColorBrush timeLabelColor;

        public SolidColorBrush TimeLabelColor
        {
            get { return timeLabelColor ?? (timeLabelColor = new SolidColorBrush()); }
            set { timeLabelColor = value; RaisePropertyChanged(nameof(TimeLabelColor)); }
        }

        //Set the new SelectedItem in "AgendaViewModelCollection" and change the colors in the view accordingly
        private void CheckTimeSelected()
        {
            if (!(CheckedTime == true || (TimeHours == null || TimeMinutes == null)))
            {
                TimeLabelColor.Color = CustomColors.Info;
            }
            else if(CheckedTime == true && (TimeHours == null || TimeMinutes == null))
            {
                TimeLabelColor.Color = CustomColors.Warning;
            }
        }
        private ICommand addgroupCommand;

        public ICommand AddgroupCommand
        {
            get
            {
                return addgroupCommand = addgroupCommand ?? new CommandBase(Addgroup);
            }
        }


        private void Addgroup()
        {
            GroupViewModel gvm = new GroupViewModel();

            GroupWindow window = new GroupWindow
            {
                DataContext = gvm
            };
            gvm.OnRequestClose += (s, e) => window.Close();
            gvm.RefreshRepo += (s, e) => LoadRepo();

            window.ShowDialog();
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
        private ICommand addEventCommand;

        public ICommand AddEventCommand
        {
            get
            {
                return addEventCommand = addEventCommand ?? new CommandBase(AddEvent, CanAddEvent);
            }
        }
        private void AddEvent()
        {
            Event newEvent = new Event()
            {
                Name = Name,
                Date = new DateTime(DateYear,DateMonth,DateDay),
                PublisherId = SessionManager.CurrentUser.UserId,
                GroupId = selectedGroup.Group.GroupID

            };
            if (!CheckedTime)
            {
                newEvent.Time =  null;
            }
            else
            {
                DateTime dt = new DateTime(1800,1,1,(int)TimeHours,(int)TimeMinutes,0);
                newEvent.Time = dt;
            }
            
            newEvent.EventId = EventRepo.Instance.Insert(newEvent);

            AgendaViewModelCollection.Instance.LoadEvent.Invoke(this,EventArgs.Empty);
            //CustomMonth cm = AgendaViewModelCollection.Instance.CustomMonthList.Where(x => x.Month == newEvent.Date.Month && x.Year == newEvent.Date.Year).FirstOrDefault();

            List<CustomMonth> cmList = AgendaViewModelCollection.Instance.CustomMonthList.Where(x => x.Days.Any(y => y.DayInMonth == newEvent.Date.Day && y.Month == newEvent.Date.Month)).ToList();

            foreach (CustomMonth cm in cmList)
            {
                if (cm != null)
                {
                    cm.Days.Where(x => x.DayInMonth == newEvent.Date.Day && x.Month == newEvent.Date.Month).FirstOrDefault().UpdateListFromAvm.Invoke(this, EventArgs.Empty);
                }
            }

            CloseWindow();
        }

        public bool CanAddEvent()
        {
            if (string.IsNullOrEmpty(Name))
                return false;

            if (CheckedTime == true && (TimeHours == null || TimeMinutes == null))
                return false;

            if (SelectedGroup == null)
                return false;

            return true;
        }

        private void SetDaysListFromMonth(int value)
        {
            List<int> newDaysList = new List<int>();
            int maxDay = AgendaFolder.Calendar_Model.DicoMonthAndMaxDay.MonthMaxDay.Where(x => x.Key == value).FirstOrDefault().Value;

            if (DateTime.IsLeapYear(DateYear))
            {
                if (value == 2)
                {
                    maxDay = 29;
                }
            }
            for (int i = 1; i <= maxDay; i++)
            {
                newDaysList.Add(i);
            }

            DaysList = newDaysList;
        }


        private ICommand updateEventCommand;

        public ICommand UpdateEventCommand
        {
            get
            {
                return updateEventCommand = updateEventCommand ?? new CommandBase(UpdateEvent);
            }
        }
        private void UpdateEvent()
        {
            Event updateEvent = new Event
            {
                EventId = this.EventId,
                Name = Name,
                Date = new DateTime(DateYear, DateMonth, DateDay),
                PublisherId = SessionManager.CurrentUser.UserId,
                GroupId = selectedGroup.Group.GroupID

            };
            if (!CheckedTime)
            {
                updateEvent.Time = null;
            }
            else
            {
                DateTime dt = new DateTime(1800, 1, 1, (int)TimeHours, (int)TimeMinutes, 0);
                updateEvent.Time = dt;
            }
            ColorEventDataContext getOldDate = AgendaViewModelCollection.Instance.EventList.Where(x => x.EventView.EventId == this.EventId).FirstOrDefault();

            

            bool Updated = EventRepo.Instance.Update(updateEvent);

            AgendaViewModelCollection.Instance.LoadEvent.Invoke(this, EventArgs.Empty);
            //CustomMonth cm = AgendaViewModelCollection.Instance.CustomMonthList.Where(x => x.Month == newEvent.Date.Month && x.Year == newEvent.Date.Year).FirstOrDefault();

            List<CustomMonth> cmList = AgendaViewModelCollection.Instance.CustomMonthList.Where
            (x => x.Days.Any(y => (y.DayInMonth == updateEvent.Date.Day && y.Month == updateEvent.Date.Month ) 
            || 
            (y.DayInMonth == getOldDate.EventView.Date.Day && y.Month == getOldDate.EventView.Date.Month)))
            .ToList();

            foreach (CustomMonth cm in cmList)
            {
                if (cm != null)
                {

                    //test .foreach 
                    //List<CustomDay> newlist = 
                    cm.Days.Where(x => (x.DayInMonth == updateEvent.Date.Day && x.Month == updateEvent.Date.Month)
                    ||
                    (x.DayInMonth == getOldDate.EventView.Date.Day && x.Month == getOldDate.EventView.Date.Month))
                    .ToList()
                    .ForEach(c => c.UpdateListFromAvm.Invoke(this,EventArgs.Empty));
                    //foreach (var item in newlist)
                    //{
                    //    item.UpdateListFromAvm.Invoke(this, EventArgs.Empty);
                    //}
                }
            }

            CloseWindow();
        }

        private void SetDaysListFromYear(int value)
        {
            List<int> newDaysList = new List<int>();
            int maxDay = AgendaFolder.Calendar_Model.DicoMonthAndMaxDay.MonthMaxDay.Where(x => x.Key == DateMonth).FirstOrDefault().Value;

            if (DateTime.IsLeapYear(value))
            {
                if (DateMonth == 2)
                {
                    maxDay = 29;
                }
            }
            for (int i = 1; i <= maxDay; i++)
            {
                newDaysList.Add(i);
            }

            DaysList = newDaysList;
        }
        private void LoadRepo()
        {
            AllGroup = new ObservableCollection<GroupDataContext>();
            int agendaId = AgendaRepo.Instance.GetAll().Where(x => x.UserId == SessionManager.CurrentUser.UserId).FirstOrDefault().AgendaId;
            List<Group> grouplist = GroupRepo.Instance.GetAll().Where(x => x.AgendaId == agendaId).ToList();

            foreach (var group in grouplist)
            {
                AllGroup.Add(new GroupDataContext(group));
            }
        }
    }
}