using Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.ViewModel.SessionFolder
{
    public static  class SessionManager
    {
        private static User currentUser;

        public static User CurrentUser
        {
            get { return currentUser; }
            set
            {
                if (currentUser == null)
                {
                    currentUser = value;
                }
            }
        }
        private static DateTime currentDate;
        
        public static DateTime CurrentDate
        {
            get
            {

                if (currentDate.Year < 2000)
                {
                    currentDate = DateTime.Now;
                }
                return currentDate;

            }
            set { currentDate = value; }
        }

    }
}
