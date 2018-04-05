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
    public class AgendaRepo : BaseRepo<int, Agenda>
    {
        #region Singleton
        private static AgendaRepo _Instance;

        public static AgendaRepo Instance
        {
            get
            {
                return _Instance ?? (_Instance = new AgendaRepo());
            }
        }
        #endregion

        private AgendaRepo() : base("Agenda") { }

        protected override Agenda Selector(IDataRecord Data)
        {
            return new Agenda()
            {
                AgendaId = (int)(Data["AgendaId"]),
                Name = (string)Data["Name"],
                UserId  = (int)Data["UserId"]

            };
        }

        public override int Insert(Agenda Entity)
        {
            Command cmd = new Command($"INSERT INTO {NameTable} (Name,UserId) OUTPUT inserted.{IdTable} VALUES (@Name, @UserId);");

            cmd.AddParameter("Name", Entity.Name);
            cmd.AddParameter("UserId", Entity.UserId);

            return (int)Db.ExecuteScalar(cmd);
        }

        public override bool Update(Agenda Entity)
        {
            throw new NotImplementedException();
        }
    }
}
