using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Model
{
    public class Event
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime? Time { get; set; }
        public int GroupId { get; set; }
        public int PublisherId { get; set; }
    }
}
