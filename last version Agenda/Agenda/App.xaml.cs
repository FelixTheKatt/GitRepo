using Agenda.ViewModel.LoginFolder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Agenda
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {



        //We created this method to have control over the first instance of the main window in order to link it to
        //a view-model to assign the "window.Close()" method to the event "OnRequestClose" (see "BaseViewModel").
        void App_Startup(object sender, StartupEventArgs start)
        {
            LoginViewModel lvm = new LoginViewModel();

            MainWindow window = new MainWindow
            {
                DataContext = lvm
            };

            lvm.OnRequestClose += (s, e) => window.Close();

            window.Show();
        }
    }
}
