using Dal.Client.Model;
using Dal.Model;
using Dal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Client.Service
{
    public class UserService
    {

        #region Singleton

        private static UserService instance;

        public static UserService Instance
        {
            get { return instance ?? (instance = new UserService()); }

        }

        #endregion
        private UserService()  { }

        public UserClient GetOne (int UserId)
        {
            User u = UserRepo.Instance.GetOne(UserId);

            if (u != null)
            {
                return new UserClient()
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    Pseudo = u.Pseudo,
                    DepartementId = u.DepartementId
                };
                
            }

            return new UserClient()
            {
                UserId = -1
            };
        }

        public List<UserClient> GetAll()
        {
            List<User> u = UserRepo.Instance.GetAll();

            List<UserClient> result = new List<UserClient>();

            u.ForEach(x => result.Add(new UserClient()
            {
                UserId = x.UserId,
                Email = x.Email,
                Pseudo = x.Pseudo,
                DepartementId = x.DepartementId
            }));
            return result;
        }

    }
}
