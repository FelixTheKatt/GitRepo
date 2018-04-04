using Agenda.Resources.Styles;
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

namespace Agenda.ViewModel.AgendaFolder.Calendar
{
    public class CustomDay : ViewModelBase
    {
        public CustomDay(int day, int month, int year)
        {
            DayInMonth = day;
            Month = month;
            Year = year;

            SelectedEvent += (s, e) => GetInstanceSelected();

            UpdateListFromAvm += (s, e) => SetListfromAVM();

            SetListfromAVM();

            //Set the default border color in the view
            CustomBackgroundColor = new SolidColorBrush();
            CustomBackgroundColor.Color = CustomColors.DefaultSelected;
            

        }

        private void SetListfromAVM()
        {
            ObservableCollection<ColorEventDataContext> result = new ObservableCollection<ColorEventDataContext>();

            List<ColorEventDataContext> list = AgendaViewModelCollection.Instance.EventList.Where(x => x.EventView.Date.Day == DayInMonth && x.EventView.Date.Month == Month && x.EventView.Date.Year == Year).ToList();

            foreach (var item in list)
            {
                result.Add(item);
            }

            EventList = result;
        }

        public EventHandler SelectedEvent;

        public EventHandler UpdateListFromAvm;

        //Range from 1 to 31
        public int DayInMonth { get; set; }
        public int DayInWeek { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        private CustomDay customDayDataContext;

        public CustomDay CustomDayDataContext
        {
            get { return customDayDataContext ?? (customDayDataContext = this); }
            //set { customDayDataContext = value; }
        }

        public ObservableCollection<ColorEventDataContext> eventList;
        public ObservableCollection<ColorEventDataContext> EventList
        {
            get
            {
                
               return eventList; 
            }

            set
            {
                eventList = value;
                RaisePropertyChanged(nameof(EventList));
            }
        }

        private SolidColorBrush customBackgroundColor;

        public SolidColorBrush CustomBackgroundColor
        {
            get { return customBackgroundColor; }
            set { customBackgroundColor = value; RaisePropertyChanged(nameof(CustomBackgroundColor)); }
        }

        //Set the new SelectedItem in "AgendaViewModelCollection" and change the colors in the view accordingly
        private void GetInstanceSelected()
        {
            if (AgendaViewModelCollection.Instance.SelectedItem != null)
            {
                AgendaViewModelCollection.Instance.SelectedItem.CustomBackgroundColor.Color = CustomColors.DefaultSelected;
            }

            AgendaViewModelCollection.Instance.SelectedItem = this;
            AgendaViewModelCollection.Instance.SelectedItem.CustomBackgroundColor.Color = CustomColors.Selected;

        }
    }
}
