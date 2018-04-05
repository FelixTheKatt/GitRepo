using Agenda.Utils.Enums;
using Agenda.ViewModel.RegisterFolder;
using Agenda.ViewModel.SessionFolder;
using Agenda.Utils;
using Agenda.Windows.Agenda;
using Agenda.Windows.Register;
using Dal.Model;
using Dal.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToolboxMvvm.wpf;
using Agenda.ViewModel.AgendaFolder.Calendar_Model;
using Agenda.ViewModel.AgendaFolder;

namespace Agenda.ViewModel.LoginFolder
{
    public class LoginViewModel : ViewModelBase
    {
        private ObservableCollection<User> allUser;

        public LoginViewModel()
        {
            LoadRepo();
        }

        public LoginViewModel(User user)
        {
            id = user.UserId;
            pseudo = user.Pseudo;
            password = user.Password;
        }


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
        private string pseudo;

        public string Pseudo
        {
            get { return pseudo; }
            set
            {
                if (pseudo != value)
                {
                    pseudo = value;
                    RaisePropertyChanged(nameof(Pseudo));
                }
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    RaisePropertyChanged(nameof(Password));
                }

            }
        }

        private string warning;

        public string Warning
        {
            get { return warning; }
            set
            {
                if (warning != value)
                {
                    warning = value;
                    RaisePropertyChanged(nameof(Warning));
                }
            }
        }


        private ICommand registerCommand;

        public ICommand RegisterCommand
        {
            get
            {
                return registerCommand = registerCommand ?? new CommandBase(Register);
            }
        }

        private ICommand loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                return loginCommand = loginCommand ?? new CommandBase(Login);
            }
        }
        private void Login()
        {
            // for test purposes
            //MonthHelper.CreateMonth(DateTime.Now);

            string pass = "";

            Guid Salt;
            
            User u = allUser.Where(x => x.Pseudo == Pseudo).FirstOrDefault();

            if (u != null)
            {
                Salt = u.Salt;
               
                pass = PasswordHasher.Hash(Password, Salt);

                if (allUser.Where(x => x.Password == pass).FirstOrDefault() != null)
                {
                    SessionManager.CurrentUser = u;

                    //NE PAS TOUCHER !!!!!!!!
                    
                    AgendaViewModelCollection avm = AgendaViewModelCollection.Instance;

                    avm.LoadEvent.Invoke(this, EventArgs.Empty);

                    AgendaWindow window = new AgendaWindow
                    {
                        DataContext = avm
                    };

                    //avm. = AgendaRepo.Instance.GetAll().Where(x => x.UserId == u.UserId).FirstOrDefault().AgendaId;
                    //avm.Date = DateTime.Now;



                    CloseWindow();

                    window.Show();

                }

                else
                {
                    Warning = WarningUtils.ErrorCodes.Where(x => x.Key == "DontExist").FirstOrDefault().Value;
                }
            }


            else
            {
                Warning = WarningUtils.ErrorCodes.Where(x => x.Key == "DontExist").FirstOrDefault().Value;

                if (string.IsNullOrEmpty(Pseudo))
                {
                    Warning = WarningUtils.ErrorCodes.Where(x => x.Key == "Null").FirstOrDefault().Value;
                }

                if (StringChecker.HasSpaces(Pseudo))
                {
                    Warning = WarningUtils.ErrorCodes.Where(x => x.Key == "NoSpace").FirstOrDefault().Value;
                }                                         
            }

        }
     
        private void Register()
        {
            RegisterViewModel rvm = new RegisterViewModel();

            RegisterWindow window = new RegisterWindow
            {
                DataContext = rvm
            };

            //Used to stock the LoadRepo(called only when creating a new LoginViewModel instance) method 
            //to execute it later(when a new user is successfully registered) to reload the database
            //in order to login immediately after registration
            rvm.updateRepo += (s, e) => LoadRepo();

            rvm.OnRequestClose += (s, e) => window.Close();


            window.ShowDialog();


            if (SessionManager.CurrentUser != null)
            {
                Pseudo = SessionManager.CurrentUser.Pseudo; 
            }

            //DOESN'T WORK
            //Password = SessionManager.CurrentUser.Password; 

        }

        //Used to avoid opening the database too often
        private void LoadRepo()
        {
            allUser = new ObservableCollection<User>();

            foreach (var user in UserRepo.Instance.GetAll())
            {
                allUser.Add(user);
            }
        }
    }
}
