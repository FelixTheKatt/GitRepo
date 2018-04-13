using Agenda.Utils.Enums;
using Agenda.ViewModel.SessionFolder;
using Dal.Client.Model;
using Dal.Client.Service;
using Dal.Model;
using Dal.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToolboxMvvm.wpf;

namespace Agenda.ViewModel.FriendsFolder
{
    public class FriendViewModel : ViewModelCollectionBase<UserClient>
    {

        #region Instance
        public  FriendViewModel Instance
        {
            get
            {
                return this;
            }
        }
        #endregion

        private List<Group> allowGroup;

        public List<Group> AllowGroup
        {
            get { return allowGroup; }
            set
            {
                allowGroup = value;
                RaisePropertyChanged(nameof(AllowGroup));
            }
        }

        public void SetGroupToColumns()
        {
            int agendaId = AgendaRepo.Instance.GetAll().Where(x => x.UserId == SessionManager.CurrentUser.UserId).FirstOrDefault().AgendaId;
            List<Group> listGroup = GroupRepo.Instance.GetAll().Where(x => x.AgendaId == agendaId).ToList();

            ObservableCollection<Group> Columns = new ObservableCollection<Group>();

            listGroup.ForEach(x => Columns.Add(x));

        }

        public FriendViewModel()
        {
            SetGroupToColumns();
        }

        public ObservableCollection<Group> Columns { get; private set; }

        private EventHandler showInviteHandler;

        public EventHandler ShowInviteHandler
        {
            get
            {
                if (showInviteHandler == null)
                {
                    showInviteHandler += (s, e) => RaisePropertyChanged(nameof(NewInvitations));
                }

                return showInviteHandler;
            }
        }

        private EventHandler updateFriendsList;

        public EventHandler UpdateFriendsList
        {
            get
            {
              
                if (updateFriendsList == null)
                {
                    updateFriendsList += (s, e) => GetFriends();
                    updateFriendsList += (s, e) => GetFutureFriends();
                }

                return updateFriendsList;
            }
        }

        private List<FriendsViewModelDataContext> myFriends;

        public List<FriendsViewModelDataContext> MyFriends
        {
            get
            {
                if (myFriends == null)
                {
                    myFriends = new List<FriendsViewModelDataContext>();
                    GetFriends();
                }
                return myFriends;
            }
            set
            {
                myFriends = value;
                RaisePropertyChanged(nameof(MyFriends));
            }
        }

        private List<FriendsViewModelDataContext> futureFriends;

        public List<FriendsViewModelDataContext> FutureFriends
        {
            get
            {
                if (futureFriends == null)
                {
                    futureFriends = new List<FriendsViewModelDataContext>();
                    GetFutureFriends();
                }
                return futureFriends;
            }
            set
            {
                myFriends = value;
                RaisePropertyChanged(nameof(MyFriends));
            }
        }

        private string search;

        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                RaisePropertyChanged(nameof(Search));
            }
        }


        public string WarningSearch
        {
            get
            {
                if (MyFriends.Count == 0 && !string.IsNullOrEmpty(Search))
                {
                    return WarningUtils.ErrorCodes.Where(x => x.Key == "NoFind").FirstOrDefault().Value;
                }
                return "";
            }
        }

        public int NewInvitations
        {
            get
            {
                List<Friends> friends = FriendsRepo.Instance.GetAll().Where(x => x.InvitationOnGoing && x.UserId2 == SessionFolder.SessionManager.CurrentUser.UserId).ToList();
                if (friends.Count == 0)
                {

                    return 0;

                }

                return 200;
            }
        }

        private ICommand refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                return refreshCommand = refreshCommand ?? new CommandBase(GetFriends);
            }
        }



        private void GetFriends()
        {
            List<Friends> friends = FriendsRepo.Instance.GetAll().Where(x => x.IsFriend && x.UserId1 == SessionFolder.SessionManager.CurrentUser.UserId).ToList();

            List<FriendsViewModelDataContext> Temp = new List<FriendsViewModelDataContext>();

            friends.ForEach(x => Temp.Add(new FriendsViewModelDataContext(UserService.Instance.GetOne(x.UserId2), false, this)));

            myFriends = Temp;
            RaisePropertyChanged(nameof(MyFriends));

        }

        private ICommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return searchCommand = searchCommand ?? new CommandBase(GetNewFriends);
            }
        }


        private void GetNewFriends()
        {
            List<UserClient> u = UserService.Instance.GetAll();

            List<FriendsViewModelDataContext> t = new List<FriendsViewModelDataContext>();
            u.ForEach(x => t.Add(new FriendsViewModelDataContext(UserService.Instance.GetOne(x.UserId), true, this)));
            t.Remove(t.Where(x => x.User.UserId == SessionFolder.SessionManager.CurrentUser.UserId).FirstOrDefault());

            if (string.IsNullOrEmpty(Search))
            {               
                MyFriends = t;
            }
            else
            {
                MyFriends = t.Where(x => x.Pseudo.ToLower().Contains(Search.ToLower())).ToList();

            }

            RaisePropertyChanged(nameof(WarningSearch));
        }

        private void GetFutureFriends()
        {
            List<Friends> friends = FriendsRepo.Instance.GetAll().Where(x => x.InvitationOnGoing && x.UserId2 == SessionFolder.SessionManager.CurrentUser.UserId).ToList();

            List<FriendsViewModelDataContext> Temp = new List<FriendsViewModelDataContext>();

            friends.ForEach(x => Temp.Add(new FriendsViewModelDataContext(UserService.Instance.GetOne(x.UserId1), true, this)));

            futureFriends = Temp;
            RaisePropertyChanged(nameof(FutureFriends));

        }

        public override ObservableCollection<UserClient> LoadItems()
        {
            return new ObservableCollection<UserClient>();
        }
    }
}
