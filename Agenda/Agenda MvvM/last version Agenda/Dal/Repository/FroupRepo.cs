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
    public class FroupRepo : BaseRepo<int, Froup>
    {
        #region Singleton
        private static FroupRepo instance;
        public static FroupRepo Instance { get => instance ?? (instance = new FroupRepo()); }
        private FroupRepo() : base("Froup") { }
        #endregion

        public override int Insert(Froup Entity)
        {
            Command cmd = new Command($"INSERT INTO {NameTable} (FriendsId, GroupId) OUTPUT inserted.{IdTable} VALUES (@FriendsId, @GroupId);");

            cmd.AddParameter("FriendsId", Entity.FriendsId);
            cmd.AddParameter("GroupId", Entity.GroupId);

            return (int)Db.ExecuteScalar(cmd);
        }

        public override bool Update(Froup Entity)
        {
            throw new NotImplementedException();
        }

        protected override Froup Selector(IDataRecord Data)
        {
            return new Froup()
            {
                FroupId = (int)(Data["FroupId"]),
                FriendsId = (int)Data["FriendsId"],
                GroupId = (int)Data["GroupId"]
            };
        }
    }
}
