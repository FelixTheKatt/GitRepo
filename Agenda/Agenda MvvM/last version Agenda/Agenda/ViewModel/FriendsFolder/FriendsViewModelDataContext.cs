using Dal.Client.Model;
using Dal.Model;
using Dal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToolboxMvvm.wpf;

namespace Agenda.ViewModel.FriendsFolder
{
    public class FriendsViewModelDataContext : ViewModelBase
    {

        
        private bool isSearched;

        public bool IsSearched
        {
            get { return isSearched; }
        }

        public string ButtonText
        {
            get { return isSearched ? "Send Invatation" : "Delete THIS MORRON" ; }

        }

        public string Pseudo
        {
            get { return User.Pseudo; }

        }
       
        public string Departement
        {
            get
            {
                return DepartementRepo.Instance.GetOne(User.DepartementId).Name;

            }
        }

        public string Email
        {
            get
            {
                return  User.Email;

            }
        }

        public FriendsViewModelDataContext() { }

        public FriendsViewModelDataContext(UserClient u,bool isSearched)
        {
            this.User = u;
            this.isSearched = isSearched;
        }

        public FriendsViewModelDataContext DataContext { get =>  this; }

        public UserClient User { get; set; }


        private ICommand addOrDeleteCommand;
        public ICommand AddOrDeleteCommand
        {
            get
            {
                if (isSearched)
                {
                    return addOrDeleteCommand = addOrDeleteCommand ?? new CommandBase(AddFriends);
                }
                else
                {
                    return addOrDeleteCommand = addOrDeleteCommand ?? new CommandBase(DeleteFriends);
                }
               
            }
        }

        private void DeleteFriends()
        {
            Friends f = FriendsRepo.Instance.GetAll().Where(x => x.UserId1 == SessionFolder.SessionManager.CurrentUser.UserId && x.UserId2 == User.UserId).FirstOrDefault();

            if (f == null)
            {

                throw new ArgumentNullException();

            }

            else
            {
                f.InvitationOnGoing = false;
                f.IsFriend = false;

                FriendsRepo.Instance.Update(f);
            }
        }

        private void AddFriends()
        {
            Friends f = FriendsRepo.Instance.GetAll().Where(x => x.UserId1 == SessionFolder.SessionManager.CurrentUser.UserId && x.UserId2 == User.UserId).FirstOrDefault();

            if (f == null)
            {
                
                f = new Friends() {UserId1= SessionFolder.SessionManager.CurrentUser.UserId, UserId2 = User.UserId , IsFriend = false, InvitationOnGoing = true };
                FriendsRepo.Instance.Insert(f);
                
            }

            else
            {
                f.InvitationOnGoing = true;
                f.IsFriend = false;

                FriendsRepo.Instance.Update(f);
            }
        }


        private ICommand acceptFriendsCommand;
        public ICommand AcceptFriendsCommand
        {
            get
            {
                return acceptFriendsCommand = acceptFriendsCommand ?? new CommandBase(AcceptFriends);
            }
        }

        private void AcceptFriends ()
        {
            Friends f = FriendsRepo.Instance.GetAll().Where(x => x.UserId2 == SessionFolder.SessionManager.CurrentUser.UserId && x.UserId1 == User.UserId).FirstOrDefault();

            f.InvitationOnGoing = false;
            f.IsFriend = true;

            FriendsRepo.Instance.Update(f);
        }

    }
}
