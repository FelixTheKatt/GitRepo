using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolBox.Connections.Database;

namespace Dal.Repository
{
    public abstract class BaseRepo<TKey, TEntity>
    {
        private const string CONNECTION_STRING =
           @"Data Source = DESKTOP-4PDG0O0; Initial Catalog = Agenda; Integrated Security = True;";
        // connection peut etre CA -PC\DESKTOP-4PDG0O0;

        private Connection _Db;
        private string _NameTable;
        private string _IdTable;

        protected Connection Db
        {
            get { return _Db; }
            private set { _Db = value; }
        }

        protected string NameTable
        {
            get { return _NameTable; }
            private set { _NameTable = value; }
        }

        protected string IdTable
        {
            get { return _IdTable; }
            private set { _IdTable = value; }
        }

        public BaseRepo(string Table)
        {
            this.NameTable = $"[{Table}]";
            this.IdTable = $"{Table}Id";
            this.Db = new Connection(CONNECTION_STRING);
        }

        //Methode pour convertir les items de la DB en object
        protected abstract TEntity Selector(IDataRecord Data);


        public List<TEntity> GetAll()
        {
            Command cmd = new Command($"SELECT * FROM {NameTable}");
            return Db.ExecuteReader(cmd, Selector).ToList();
        }

        public TEntity GetOne(TKey id)
        {
            Command cmd = new Command($"SELECT * FROM {NameTable} WHERE {IdTable} = @Id");
            cmd.AddParameter("Id", id);

            return Db.ExecuteReader(cmd, Selector).FirstOrDefault();
        }

        public bool Delete(TKey id)
        {
            Command cmd = new Command($"DELETE FROM {NameTable} WHERE {IdTable} = @Id");
            cmd.AddParameter("Id", id);

            return (Db.ExecuteNonQuery(cmd) == 1);
        }

        public abstract TKey Insert(TEntity Entity);
        public abstract bool Update(TEntity Entity);

    }
}

