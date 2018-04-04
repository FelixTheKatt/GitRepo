using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.ViewModel.GroupFolder
{
    public class EventDetails
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime? Time { get; set; }
        public int GroupId { get; set; }

        public string DateNoTime
        {
            get
            {
                return Date.Day.ToString("00") + "/" + Date.Month.ToString("00") +"/"+ Date.Year.ToString();
            }
        }
        public string TimeNoDate {
            get
            {
                if (Time == null)
                {
                    return null;
                }
                else
                {
                    DateTime dt = (DateTime)Time;
                    return dt.Hour.ToString("00") + ":" + dt.Minute.ToString("00");
                  
                }          
            }

        }
    }
}
