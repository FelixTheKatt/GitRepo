using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Model
{
    public class Group
    {
        public int GroupID { get; set; }
        public string Name { get; set; }
        public string RGBAColor { get; set; }
        public int AgendaId { get; set; }
    }
}
