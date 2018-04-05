using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows;

namespace ToolboxMvvm.wpf
{

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        

        // This right here is black magic
        public event EventHandler OnRequestClose;

        protected virtual void CloseWindow()
        {
            EventHandler handler = OnRequestClose;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        //End of the black magic
    }
}