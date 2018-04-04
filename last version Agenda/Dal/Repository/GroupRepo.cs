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
    public class GroupRepo : BaseRepo<int,Group>
    {
        #region Singleton
        private static GroupRepo _Instance;

        public static GroupRepo Instance
        {
            get
            {
                return _Instance ?? (_Instance = new GroupRepo());
            }
        }
        #endregion

        private GroupRepo() : base("Group") { }

        protected override Group Selector(IDataRecord Data)
        {
            return new Group()
            {
                GroupID= (int)(Data["GroupID"]),
                Name = (string)Data["Name"],
                RGBAColor = (string)Data["RGBAColor"],
                AgendaId = (int)Data["AgendaId"]

            };
        }

        public override int Insert(Group Entity)
        {
            Command cmd = new Command($"INSERT INTO {NameTable} (Name,RGBAColor,AgendaId) OUTPUT inserted.{IdTable} VALUES (@Name, @RGBAColor,@AgendaId);");

            cmd.AddParameter("Name", Entity.Name);
            cmd.AddParameter("RGBAColor", Entity.RGBAColor);
            cmd.AddParameter("AgendaId", Entity.AgendaId);

            return (int)Db.ExecuteScalar(cmd);
        }

        public override bool Update(Group Entity)
        {
            throw new NotImplementedException();
        }
    }
}
