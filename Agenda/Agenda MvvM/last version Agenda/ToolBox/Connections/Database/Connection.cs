using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ToolBox.Connections.Database
{
    public class Connection
    {
        private SqlConnection db;
        private string _ConnectionString;

        public string ConnectionString
        {
            get { return _ConnectionString; }
        }

        public Connection(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
            db = new SqlConnection(ConnectionString);
        }

        private SqlCommand CreateSqlCommand(Command command)
        {
            SqlCommand cmd = db.CreateCommand();

            cmd.CommandText = command.Query;

            foreach (KeyValuePair<string, object> kvp in command.Parameters)
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = kvp.Key;
                param.Value = kvp.Value;

                cmd.Parameters.Add(param);
            }

            return cmd;
        }

        public object ExecuteScalar(Command command)
        {
            SqlCommand cmd = CreateSqlCommand(command);
            object result;

            try
            {
                db.Open();
                result = cmd.ExecuteScalar(); 
            }
            finally
            {
                db.Close();
            }

            return result;
        }

        public int ExecuteNonQuery(Command command)
        {
            int result = -1;
            SqlCommand cmd = CreateSqlCommand(command);

            try
            {
                db.Open();
                result = cmd.ExecuteNonQuery();
            }
            finally
            {
                db.Close();
            }

            return result;
        }

        public IEnumerable<T> ExecuteReader<T>(Command command, Func<IDataRecord, T> converter)
        {
            if (converter == null) throw new ArgumentNullException();

            SqlCommand cmd = CreateSqlCommand(command);

            try
            {
                db.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    yield return converter(reader);
                }
            }
            finally
            {
                db.Close();
            }
        }

    }
}
