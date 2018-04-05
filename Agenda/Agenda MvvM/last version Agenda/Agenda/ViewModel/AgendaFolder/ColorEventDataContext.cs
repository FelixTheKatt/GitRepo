using Agenda.Resources.Styles;
using Agenda.ViewModel.AgendaFolder.Calendar;
using Dal.Model;
using Dal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ToolboxMvvm.wpf;

namespace Agenda.ViewModel.AgendaFolder
{
    public class ColorEventDataContext
    {
        public Event EventView { get; set; }
        public string Name
        {
            get
            {
                return EventView.Name;
            }
        }
        public SolidColorBrush GroupColorAVM
        {
            get
            {
                Group group =  GroupRepo.Instance.GetOne(EventView.GroupId);
                return new SolidColorBrush(CustomColors.FromString(group.RGBAColor));
            }
        }
        public ColorEventDataContext DataContext
        {
            get { return this; }
        }
        public ColorEventDataContext(Event eventView)
        {
            EventView = eventView;
        }

        private ICommand deleteEventCommand;

        public ICommand DeleteEventCommand
        {
            get
            {
                return deleteEventCommand = deleteEventCommand ?? new CommandBase(DeleteEvent);
            }
        }


        private void DeleteEvent()
        {
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the event {EventView.Name} ?", "Warning", MessageBoxButton.YesNo);

            if(result == MessageBoxResult.Yes)
            {
                EventRepo.Instance.Delete(EventView.EventId);

                AgendaViewModelCollection.Instance.LoadEvent?.Invoke(this, EventArgs.Empty);

                List<CustomMonth> cmList = AgendaViewModelCollection.Instance.CustomMonthList.Where(x => x.Days.Any(y => y.DayInMonth == EventView.Date.Day && y.Month == EventView.Date.Month)).ToList();

                foreach (CustomMonth cm in cmList)
                {
                    if (cm != null)
                    {
                        cm.Days.Where(x => x.DayInMonth == EventView.Date.Day && x.Month == EventView.Date.Month).FirstOrDefault().UpdateListFromAvm.Invoke(this, EventArgs.Empty);
                    }
                }
            }            
        }
    }
}
