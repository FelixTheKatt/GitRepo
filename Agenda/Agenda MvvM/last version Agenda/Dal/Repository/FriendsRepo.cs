using Dal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox.Connections.Database;

namespace Dal.Repository
{
    public class FriendsRepo : BaseRepo<int, Friends>
    {
        #region Singleton
        private static FriendsRepo instance;
        public static FriendsRepo Instance { get => instance ?? (instance = new FriendsRepo()); }
        private FriendsRepo() : base("Friends") { } 
        #endregion

        public override int Insert(Friends Entity)
        {
            Command cmd = new Command($"INSERT INTO {NameTable} (UserId1, UserId2) OUTPUT inserted.{IdTable} VALUES (@UserId1, @UserId2);");

            cmd.AddParameter("UserId1", Entity.UserId1);
            cmd.AddParameter("UserId2", Entity.UserId2);

            return (int)Db.ExecuteScalar(cmd);
        }

        public override bool Update(Friends Entity)
        {
            Command cmd = new Command($"Update {NameTable} set CheckUser = @CheckUser WHERE {IdTable} = @FriendsId");


            cmd.AddParameter("CheckUser", Entity.CheckUser);
            cmd.AddParameter("FriendsId", Entity.FriendsId);

            return (Db.ExecuteNonQuery(cmd) == 1);
        }

        protected override Friends Selector(IDataRecord Data)
        {
            return new Friends()
            {
                FriendsId = (int)(Data["FriendsId"]),
                UserId1 = (int)Data["UserId1"],
                UserId2 = (int)Data["UserId2"],
                CheckUser = (bool)Data["CheckUser"]
            };
        }
    }
}
