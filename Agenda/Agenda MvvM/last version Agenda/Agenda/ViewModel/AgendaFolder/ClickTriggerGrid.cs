using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;


//Useless for now
namespace Agenda.ViewModel.AgendaFolder
{
    public class ClickTriggerGrid : EventTriggerBase<FrameworkElement>
    {
        protected override string GetEventName()
        {
            return Mouse.MouseDownEvent.Name;
        }

        protected override void OnAttached()
        {
            base.OnAttached();
        }
        protected override void OnEvent(EventArgs eventArgs)
        {
            var mouseEventArgs = eventArgs as MouseButtonEventArgs;
            if (mouseEventArgs == null)
            {
                return;
            }
            mouseEventArgs.Handled = true;
            base.OnEvent(eventArgs);
        }
    }
}
