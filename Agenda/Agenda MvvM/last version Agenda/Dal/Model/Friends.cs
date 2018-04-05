using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Model
{
    public class Friends
    {
        public int FriendsId { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public bool CheckUser { get; set; }
    }
}
