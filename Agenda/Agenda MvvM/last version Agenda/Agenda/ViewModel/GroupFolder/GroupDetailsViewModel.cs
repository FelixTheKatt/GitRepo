using Agenda.Resources.Styles;
using Agenda.ViewModel.AgendaFolder;
using Dal.Model;
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
    public class GroupDetailsViewModel : ViewModelBase
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

        private bool toggleHistory;

        public bool ToggleHistory
        {
            get { return toggleHistory; }
            set
            {
                toggleHistory = value;
                RaisePropertyChanged(nameof(ToggleHistory));
            }
        }



        private List<EventDetails> eventList;
        public List<EventDetails> EventList
        {
            get
            {
                if (eventList == null)
                {
                    List<EventDetails> result = new List<EventDetails>();

                    foreach (ColorEventDataContext e in AgendaViewModelCollection.Instance.EventList.Where(x => x.EventView.GroupId == Group.GroupID))
                    {
                        result.Add(new EventDetails()
                        {
                            GroupId = e.EventView.GroupId,
                            Name = e.EventView.Name,
                            Date = e.EventView.Date,
                            Time = e.EventView.Time,
                            EventId = e.EventView.EventId
                        });
                    }

                    if (ToggleHistory == false)
                    {
                        List<EventDetails> e = new List<EventDetails>();
                        foreach (var item in result)
                        {
                            DateTime dt = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day);
                            if (dt >= DateTime.Now.Date)
                            {
                                e.Add(item);
                            }
                        }

                        eventList = e.OrderBy(x => x.Date).ThenBy(x => x.Time).ToList();
                    }
                    else
                    {
                        eventList = result.OrderBy(x => x.Date).ThenBy(x => x.Time).ToList();
                    }

                }

                return eventList;
            }

            set
            {
                eventList = value;
                RaisePropertyChanged(nameof(EventList));
            }
        }
        private ICommand historyCommand;

        public ICommand HistoryCommand
        {
            get
            {
                return historyCommand = historyCommand ?? new CommandBase(SetEventList);
            }
        }

        private void SetEventList()
        {
            ToggleHistory = !ToggleHistory;
            List<EventDetails> result = new List<EventDetails>();
            foreach (ColorEventDataContext e in AgendaViewModelCollection.Instance.EventList.Where(x => x.EventView.GroupId == Group.GroupID))
            {
                result.Add(new EventDetails()
                {
                    GroupId = e.EventView.GroupId,
                    Name = e.EventView.Name,
                    Date = e.EventView.Date,
                    Time = e.EventView.Time,
                    EventId = e.EventView.EventId
                });
            }

            if (ToggleHistory == false)
            {
                List<EventDetails> e = new List<EventDetails>();
                foreach (var item in result)
                {
                    DateTime dt = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day);
                    if (dt >= DateTime.Now.Date)
                    {
                        e.Add(item);
                    }
                }

                EventList = e.OrderBy(x => x.Date).ThenBy(x => x.Time).ToList();
            }
            else
            {
                EventList = result.OrderBy(x => x.Date).ThenBy(x => x.Time).ToList();
            }

        }
    }
}
