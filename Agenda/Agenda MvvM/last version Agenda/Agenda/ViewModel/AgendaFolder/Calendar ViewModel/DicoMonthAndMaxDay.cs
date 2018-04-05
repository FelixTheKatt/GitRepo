using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.ViewModel.AgendaFolder.Calendar_Model
{
    public static class DicoMonthAndMaxDay
    {
        public static readonly Dictionary<int, int> MonthMaxDay
       = new Dictionary<int, int>
        {

           //Month and MaxDay
           // 1 is january , 31 is number of days
             { 1, 31 },
             { 2, 28 },
             { 3, 31 },
             { 4, 30 },
             { 5, 31 },
             { 6, 30 },
             { 7, 31 },
             { 8, 31 },
             { 9, 30 },
             { 10, 31 },
             { 11, 30 },
             { 12, 31 }
        };
    }
}
