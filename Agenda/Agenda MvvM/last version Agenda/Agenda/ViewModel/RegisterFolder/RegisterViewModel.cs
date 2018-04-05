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
using System.Windows;
using System.Text.RegularExpressions;
using Agenda.Utils.Enums;
using Agenda.ViewModel.SessionFolder;
using Agenda.Utils;

namespace Agenda.ViewModel.RegisterFolder
{
    public class RegisterViewModel : ViewModelBase
    {
        //public event EventHandler OnRequestClose;

        
        public event EventHandler updateRepo;


        #region Fields and properties
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
                    RaisePropertyChanged(nameof(WarningPseudo));

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
                    RaisePropertyChanged(nameof(WarningPassword));
                }

            }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    RaisePropertyChanged(nameof(Email));
                    RaisePropertyChanged(nameof(WarningEmail));
                }

            }
        }
        private Departement selectedDepartement;

        public Departement SelectedDepartement
        {
            get { return selectedDepartement; }
            set
            {
                if (selectedDepartement != value)
                {
                    selectedDepartement = value;
                    RaisePropertyChanged(nameof(SelectedDepartement));
                }

            }
        }


        private ObservableCollection<User> allUser;

        private ObservableCollection<Departement> allDepartement;

        public ObservableCollection<Departement> AllDepartement
        {
            get { return allDepartement ?? (allDepartement = GetallDepartement()); }
        }

        public string WarningPseudo
        {
            get
            {
                if (string.IsNullOrEmpty(Pseudo))
                {
                    return WarningUtils.ErrorCodes.Where(x => x.Key == "Null").FirstOrDefault().Value;
                }

                if (StringChecker.HasSpaces(Pseudo))
                {
                    return WarningUtils.ErrorCodes.Where(x => x.Key == "NoSpace").FirstOrDefault().Value;
                }

                if (PseudoAlreadyExist())
                {
                    return WarningUtils.ErrorCodes.Where(x => x.Key == "AlreadyExist").FirstOrDefault().Value;
                }

                return "";
            }
        }

        public string WarningPassword
        {
            get
            {
                if (string.IsNullOrEmpty(Password))
                {
                    return WarningUtils.ErrorCodes.Where(x => x.Key == "Null").FirstOrDefault().Value;
                }

                if (StringChecker.HasSpaces(Password))
                {
                    return WarningUtils.ErrorCodes.Where(x => x.Key == "NoSpace").FirstOrDefault().Value;
                }

                return "";
            }
        }

        public string WarningEmail
        {
            get
            {
                if (string.IsNullOrEmpty(Email))
                {
                    return WarningUtils.ErrorCodes.Where(x => x.Key == "Null").FirstOrDefault().Value;
                }

                if (!StringChecker.IsValidEmail(Email))
                {
                    return WarningUtils.ErrorCodes.Where(x => x.Key == "EmailInvalid").FirstOrDefault().Value;
                }

                if (EmailAlreadyExist())
                {
                    return WarningUtils.ErrorCodes.Where(x => x.Key == "AlreadyExist").FirstOrDefault().Value;
                }

                return "";
            }
        }
        #endregion

        #region Constructor
        public RegisterViewModel() { LoadRepo(); }

        //public RegisterViewModel(User user)
        //{
        //    id = user.UserId;
        //    pseudo = user.Pseudo;
        //    password = user.Password;
        //    email = user.Email;
        //    selectedDepartement.DepartementId = user.DepartementId;
        //} 
        #endregion

        #region Commands
        private ICommand registrationConfirmCommand;

        public ICommand RegistrationConfirmCommand
        {
            get
            {
                return registrationConfirmCommand = registrationConfirmCommand ?? new CommandBase(RegisterConfirm, CanRegisterConfirm);
            }
        }

        private ICommand cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                return cancelCommand = cancelCommand ?? new CommandBase(Cancel);
            }
        }
        #endregion

        #region Button functions

        private void RegisterConfirm()
        {
            User newUser = new User()
            {
                Pseudo = Pseudo,
                Password = Password,
                Email = Email,
                DepartementId = SelectedDepartement.DepartementId

            };

            newUser.UserId = UserRepo.Instance.Insert(newUser);



            SessionManager.CurrentUser = newUser;

            Dal.Model.Agenda agenda = new Dal.Model.Agenda()
            {
                Name = "Agenda",
                UserId = newUser.UserId
            };




            //Execute the LoadRepo method FROM THE LOGINVIEWMODEL to be able to log in immediatly after registration
            updateRepo?.Invoke(this, EventArgs.Empty);

            int AgendaId = AgendaRepo.Instance.Insert(agenda);

            //Create group by default because each event needs a group
            Dal.Model.Group group = new Dal.Model.Group()
            {
                Name = "DefaultGroup",
                AgendaId = AgendaId,
                RGBAColor = "255150150150"
               
            };
            GroupRepo.Instance.Insert(group);

            CloseWindow();

        }
        public bool CanRegisterConfirm()
        {

            if (string.IsNullOrEmpty(Pseudo) || StringChecker.HasSpaces(Pseudo))
            {
                return false;
            }

            if (!StringChecker.IsValidEmail(Email))
            {
                return false;
            }

            if (string.IsNullOrEmpty(Password) || StringChecker.HasSpaces(Password))
            {
                return false;
            }


            if (SelectedDepartement == null)
            {
                return false;
            }

            if (EmailAlreadyExist())
            {
                return false;
            }

            if (PseudoAlreadyExist())
            {
                return false;
            }

            return true;
        }

        private void Cancel()
        {
            CloseWindow();
        }
        #endregion

        #region Fonctions which open the db
        public ObservableCollection<Departement> GetallDepartement()
        {

            ObservableCollection<Departement> departementCollection = new ObservableCollection<Departement>();

            foreach (var departement in DepartementRepo.Instance.GetAll())
            {
                departementCollection.Add(departement);
            }

            return departementCollection;
        }

        private void LoadRepo()
        {
            allUser = new ObservableCollection<User>();

            foreach (var user in UserRepo.Instance.GetAll())
            {
                allUser.Add(user);
            }
        }
        #endregion

        #region Check functions
        private bool PseudoAlreadyExist()
        {

            if (allUser.Where(x => x.Pseudo == Pseudo).FirstOrDefault() != null)
            {
                return true;
            }

            return false;
        }

        private bool EmailAlreadyExist()
        {

            if (allUser.Where(x => x.Email == Email).FirstOrDefault() != null)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
