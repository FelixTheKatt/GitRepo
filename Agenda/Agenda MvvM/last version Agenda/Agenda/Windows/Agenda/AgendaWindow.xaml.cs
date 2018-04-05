using Agenda.ViewModel.AgendaFolder;
using Agenda.ViewModel.AgendaFolder.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Agenda.Windows.Agenda
{
    /// <summary>
    /// Interaction logic for AgendaWindow.xaml
    /// </summary>
    public partial class AgendaWindow : Window
    {
      
        public AgendaWindow()
        {
            InitializeComponent();    
            
        }
        
        //ONLY on grid
        private void PreviewMouseLeftButtonDown(object sender , MouseButtonEventArgs e)
        {
            //Call an event in "CustomDayViewModel"
            //We couldn't find a way to do this without code behind 
            ((sender as Grid).DataContext as CustomDay).SelectedEvent.Invoke(sender,e) ;
           
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Dal.Model.Event lolevent = ((sender as Label).DataContext as ColorEventDataContext).EventView;

            AgendaViewModelCollection.Instance.UpdateEvent(lolevent);
            
        }
    }
}
