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
    public class EventRepo : BaseRepo<int, Event>
    {
        #region Singleton
        private static EventRepo _Instance;

        public static EventRepo Instance
        {
            get
            {
                return _Instance ?? (_Instance = new EventRepo());
            }
        }
        #endregion

        private EventRepo() : base("Event") { }

        protected override Event Selector(IDataRecord Data)
        {

            Event result = new Event();

            result.EventId = (int)(Data["EventId"]);
            result.Name = (string)Data["Name"];
            result.Date = (DateTime)Data["Date"];
            result.GroupId = (int)(Data["GroupId"]);
            result.PublisherId = (int)Data["PublisherId"];

            if (Data["Time"] == DBNull.Value)
                result.Time = new DateTime?();
            else
            {
                TimeSpan ts = (TimeSpan)(Data["Time"]);

                result.Time = DateTime.MinValue.Date.Add(ts);
                //result.Time = (DateTime)Data["Time"];
            }


            return result;
        }

        public override int Insert(Event Entity)
        {
            Command cmd = new Command($"INSERT INTO {NameTable} (Name,Date,Time,GroupId, PublisherId) OUTPUT inserted.{IdTable} VALUES (@Name, @Date, @Time, @GroupId, @PublisherId);");
            
            if (Entity.Time == null)
            {
                cmd.AddParameter("Time", DBNull.Value);
            }
            else
                cmd.AddParameter("Time", Entity.Time);

            cmd.AddParameter("Name", Entity.Name);
            cmd.AddParameter("Date", Entity.Date);
            cmd.AddParameter("GroupId", Entity.GroupId);
            cmd.AddParameter("PublisherId", Entity.PublisherId);



            return (int)Db.ExecuteScalar(cmd);
        }

        public override bool Update(Event Entity)
        {
            Command cmd = new Command($"Update {NameTable} set Name=@Name, Date=@Date, Time=@Time, GroupId=@GroupId WHERE {IdTable} = @EventId");

            if (Entity.Time == null)
            {
                cmd.AddParameter("Time", DBNull.Value);
            }
            else
                cmd.AddParameter("Time", Entity.Time);

            cmd.AddParameter("EventId", Entity.EventId);
            cmd.AddParameter("Name", Entity.Name);
            cmd.AddParameter("Date", Entity.Date);
            cmd.AddParameter("GroupId", Entity.GroupId);

            return (Db.ExecuteNonQuery(cmd) == 1);
        }

    }
}
