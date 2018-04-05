using Agenda.Utils.Enums;
using Agenda.ViewModel.AgendaFolder.Calendar;
using Agenda.ViewModel.AgendaFolder.Calendar_Model;
using Agenda.ViewModel.EventFolder;
using Agenda.ViewModel.GroupFolder;
using Agenda.ViewModel.SessionFolder;
using Agenda.Windows.Event;
using Agenda.Windows.Group;
using Dal.Model;
using Dal.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ToolboxMvvm.wpf;

namespace Agenda.ViewModel.AgendaFolder
{
    public class AgendaViewModelCollection : ViewModelCollectionBase<CustomDay>
    {


        //Return the first instance created in the program
        //Can't do a singleton because a constructor would break the application
        //Used in "CustomDayViewModel"
        #region Instance
        private static AgendaViewModelCollection _Instance;

        public static AgendaViewModelCollection Instance
        {
            get
            {
                return _Instance ?? (_Instance = new AgendaViewModelCollection());
            }
        }
        #endregion

        private EventHandler loadEvent;
        public EventHandler LoadEvent
        {
            get { return loadEvent ?? (loadEvent += (s, e) => LoadRepo()); }
        }
        //LoadItems comes from Parent Class
        public override ObservableCollection<CustomDay> LoadItems()
        {
            {
                CustomMonth cm = MonthHelper.CreateMonth(DateTime.Now);
                ObservableCollection<CustomDay> dayList = new ObservableCollection<CustomDay>();

                foreach (var item in cm.Days)
                {
                    dayList.Add(item);
                }
                CustomMonthList.Add(cm);
                return dayList;
            }
        }

        //All Event from logged user
        private ObservableCollection<ColorEventDataContext> eventList;
        public ObservableCollection<ColorEventDataContext> EventList
        {
            get
            {             
                if (eventList == null)
                    eventList = new ObservableCollection<ColorEventDataContext>();

                return eventList;
            }
        }

        public string Month
        {
            get { return ((MonthEnum)SessionManager.CurrentDate.Month).ToString(); }
        }


        public int Year
        {
            get { return SessionManager.CurrentDate.Year; }
        }

        private List<CustomMonth> customMonthList;

        public List<CustomMonth> CustomMonthList
        {
            get { return customMonthList ?? (customMonthList = new List<CustomMonth>()); }
            set { customMonthList = value; }
        }

        private List<GroupDataContext> groupDatacontexts;

        public List<GroupDataContext> GroupDatacontexts
        {
            get
            {

                return groupDatacontexts ?? (groupDatacontexts = new List<GroupDataContext>()); 
            }
            set { groupDatacontexts = value; }
        }


        private ICommand nextCommand;

        public ICommand NextCommand
        {
            get
            {
                return nextCommand = nextCommand ?? new CommandBase(Next);
            }
        }

        //Add a month to the current date and refresh the displayed calendar
        private void Next()
        {
            SessionManager.CurrentDate = SessionManager.CurrentDate.AddMonths(1);
            CustomMonth cm = (customMonthList.Where(x => x.Month == SessionManager.CurrentDate.Month && x.Year == SessionManager.CurrentDate.Year).FirstOrDefault());
            if (cm == null)
            {

                 cm = MonthHelper.CreateMonth(SessionManager.CurrentDate);
            }
                ObservableCollection<CustomDay> dayList = new ObservableCollection<CustomDay>();

                foreach (var item in cm.Days)
                {
                    dayList.Add(item);
                }
                Items = dayList;
                //RaisePropertyChanged is here to update the label along with the dates themselves 
                RaisePropertyChanged(nameof(Month));
                RaisePropertyChanged(nameof(Year));

                dayList.Where(x => x.DayInMonth == 1).FirstOrDefault().SelectedEvent.Invoke(this, EventArgs.Empty);
                CustomMonthList.Add(cm);      
        }

        private ICommand previousCommand;

        public ICommand PreviousCommand
        {
            get
            {
                return previousCommand = previousCommand ?? new CommandBase(Previous);
            }
        }

        //Substract a month to the current date and refresh the displayed calendar
        private void Previous()
        {
            SessionManager.CurrentDate = SessionManager.CurrentDate.AddMonths(-1);
            CustomMonth cm = (customMonthList.Where(x => x.Month == SessionManager.CurrentDate.Month && x.Year == SessionManager.CurrentDate.Year).FirstOrDefault());
            if (cm == null)
            {

                cm = MonthHelper.CreateMonth(SessionManager.CurrentDate);
            }

            ObservableCollection<CustomDay> dayList = new ObservableCollection<CustomDay>();

            foreach (var item in cm.Days)
            {
                dayList.Add(item);
            }
            Items = dayList;
            //RaisePropertyChanged is here to update the label along with the dates themselves 
            RaisePropertyChanged(nameof(Month));
            RaisePropertyChanged(nameof(Year));

            dayList.Where(x => x.DayInMonth == 1).FirstOrDefault().SelectedEvent.Invoke(this, EventArgs.Empty);
            CustomMonthList.Add(cm);
        }

        private ICommand addEventCommand;

        public ICommand AddEventCommand
        {
            get
            {
                return addEventCommand = addEventCommand ?? new CommandBase(AddEvent);
            }
        }

        private void AddEvent ()
        {
            EventViewModel evm = new EventViewModel();
            if (SelectedItem == null)
            {
                evm.DateYear = DateTime.Now.Year;
                evm.DateMonth = DateTime.Now.Month;
                evm.DateDay = DateTime.Now.Day;
            }
            else
            {            
                evm.DateYear = SelectedItem.Year;
                evm.DateMonth = SelectedItem.Month;
                evm.DateDay = SelectedItem.DayInMonth;
            }
            EventWindow window = new EventWindow()
            {
                DataContext = evm
            };

            evm.OnRequestClose += (s, e) => window.Close();

            window.ShowDialog();
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

            window.ShowDialog();
        }

        public void UpdateEvent(Event _event)
        {
            EventViewModel evm = new EventViewModel()
            {
                EventId = _event.EventId,
                Name = _event.Name,
                DateYear = _event.Date.Year,
                DateMonth = _event.Date.Month,
                DateDay = _event.Date.Day                      
            };
            evm.SelectedGroup = evm.AllGroup.Where(x => x.Group.GroupID == _event.GroupId).FirstOrDefault();
            if (_event.Time == null)
            {
                evm.CheckedTime = false;
            }
            else
            {
                evm.CheckedTime = true;

                DateTime dt = (DateTime)_event.Time;
                evm.TimeHours = dt.Hour;
                evm.TimeMinutes = dt.Minute;
            }

            UpdateEventWindow window = new UpdateEventWindow
            {
                DataContext = evm
            };
            evm.OnRequestClose += (s, e) => window.Close();

            window.ShowDialog();
        }
        private ICommand manageGroupCommand;

        public ICommand ManageGroupCommand
        {
            get
            {
                return manageGroupCommand = manageGroupCommand ?? new CommandBase(ManageGroup);
            }
        }


        private void ManageGroup()
        {
            GroupManagerViewModel gmvm = new GroupManagerViewModel();

            GroupManagerWindow window = new GroupManagerWindow
            {
                DataContext = gmvm
            };
            int agendaId = AgendaRepo.Instance.GetAll().Where(x => x.UserId == SessionManager.CurrentUser.UserId).FirstOrDefault().AgendaId;

            List<Group> listGroup = GroupRepo.Instance.GetAll().Where(x => x.AgendaId == agendaId).ToList();

            List<GroupManagerDataContext> gmdcList = new List<GroupManagerDataContext>();

            listGroup.ForEach(x => gmdcList.Add(new GroupManagerDataContext(GroupRepo.Instance.GetOne(x.GroupID))));

            gmdcList.ForEach(x => x.GMVM = gmvm);

            gmvm.AllGroup = gmdcList;

            gmvm.OnRequestClose += (s, e) => window.Close();

            window.ShowDialog();
        }


        private void LoadRepo()
        {
            EventList.Clear();
            List<Event> list = new List<Event>();

            int agendaId = AgendaRepo.Instance.GetAll().Where(x => x.UserId == SessionManager.CurrentUser.UserId).FirstOrDefault().AgendaId;

            List<Group> grouplist = GroupRepo.Instance.GetAll().Where(x => x.AgendaId == agendaId).ToList();

            List<GroupDataContext> gdcList = new List<GroupDataContext>();

            foreach (Group group in grouplist)
            {
                GroupDataContext gdc = new GroupDataContext(group);
                List<Dal.Model.Event> eventList = EventRepo.Instance.GetAll().Where(x => x.GroupId == group.GroupID).ToList();
                gdc.NumberOfEvent = eventList.Count();
                gdcList.Add(gdc);
                list.AddRange(eventList);
            }

            foreach (Event item in list)
            {
                EventList.Add(new ColorEventDataContext(item));
            }

            GroupDatacontexts = gdcList.OrderByDescending(x => x.NumberOfEvent).Take(4).ToList();

            RaisePropertyChanged(nameof(GroupDatacontexts));

            RaisePropertyChanged(nameof(EventList));
        }
    }
}
