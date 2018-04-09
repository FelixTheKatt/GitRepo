using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Client.Model
{
    public class UserClient
    {
        public int UserId { get; set; }
        public string Pseudo { get; set; }    
        public string Email { get; set; }    
        public int DepartementId { get; set; }
    }
}
