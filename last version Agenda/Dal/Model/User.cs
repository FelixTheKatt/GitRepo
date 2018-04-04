using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Pseudo { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid Salt { get; set; }
        public int DepartementId { get; set; }
    }
}
