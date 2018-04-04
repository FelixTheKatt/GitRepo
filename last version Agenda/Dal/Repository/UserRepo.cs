using Dal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ToolBox.Connections.Database;
using ToolboxMvvm.wpf;

namespace Dal.Repository
{
    public class UserRepo : BaseRepo<int, User>
    {
        #region Singleton
        private static UserRepo _Instance;

        public static UserRepo Instance
        {
            get
            {
                return _Instance ?? (_Instance = new UserRepo());
            }
        }
        #endregion

        private UserRepo() : base("User") { }

        protected override User Selector(IDataRecord Data)
        {
            return new User()
            {
                UserId = (int)(Data["UserId"]),
                Pseudo = (string)Data["Pseudo"] ,
                Password = (string)Data["Password"],
                Email = (string)Data["Email"],
                Salt = (Guid)Data["Salt"],
                DepartementId = (int)Data["DepartementId"]
            };
        }

        public override int Insert(User Entity)
        {

            Guid salt = Salt();

            string pass = PasswordHasher.Hash(Entity.Password, salt);

            Command cmd = new Command($"INSERT INTO {NameTable} (Pseudo,Email,Password,Salt,DepartementId) OUTPUT inserted.{IdTable} VALUES (@Pseudo, @Email, @Password, @Salt, @DepartementId);");

            cmd.AddParameter("Pseudo", Entity.Pseudo);
            cmd.AddParameter("Email", Entity.Email);
            cmd.AddParameter("Password", pass);
            cmd.AddParameter("DepartementId", Entity.DepartementId);

            cmd.AddParameter("Salt", salt);

            return (int)Db.ExecuteScalar(cmd);
        }

        public override bool Update(User Entity)
        {
            throw new NotImplementedException();
        }

        private Guid Salt()
        {
            return Guid.NewGuid();
        }

    }
}
