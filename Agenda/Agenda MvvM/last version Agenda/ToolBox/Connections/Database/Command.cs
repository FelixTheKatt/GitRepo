using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.Connections.Database
{
    public class Command
    {
        private string _Query;
        private Dictionary<string, object> _Parameters;

        public string Query
        {
            get { return _Query; }
        }
        
        public Dictionary<string,object> Parameters
        {
            get { return _Parameters; }
        }

        public Command(string Query)
        {
            _Query = Query;
            _Parameters = new Dictionary<string, object>();
        }

        public void AddParameter(string Name, object Value)
        {
            _Parameters.Add(Name, Value);
        }
    }
}
