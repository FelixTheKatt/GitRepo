using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.ViewModel.AgendaFolder.Calendar
{
    public class CustomMonth
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        
        public List<CustomDay> Days { get; set; }

        public CustomMonth()
        {
            Days = new List<CustomDay>();
        }
    }
}
