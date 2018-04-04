using Agenda.ViewModel.AgendaFolder.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.ViewModel.AgendaFolder.Calendar_Model
{
    public static class MonthHelper
    {
        public static CustomMonth CreateMonth(DateTime d)
        {


            const int maxcustomDays = 42;

            CustomMonth cm = new CustomMonth();
            cm.Year = d.Year;
            cm.Month = d.Month;

            int allCreatedDays = 0;

            int dayInMonth;
            int month;
            int year;


            // Get Max day from the current month    
            int maxDays = DicoMonthAndMaxDay.MonthMaxDay.Where(x => x.Key == d.Month).FirstOrDefault().Value;

            // Set maxDay to 29 if leap year and month is February
            if (DateTime.IsLeapYear(d.Year) && d.Month == 2)
            {
                maxDays = 29;

            }
            // Maximum number of days in the month before the month from datetime parameter.
            int maxDaysLastMonth;

            //Condition for january only to prevent nullreferenceExcepetion 
            if (d.Month == 1)
            {
                maxDaysLastMonth = DicoMonthAndMaxDay.MonthMaxDay.Where(x => x.Key == d.Month).FirstOrDefault().Value;
            }
            //see maxdays above
            else
            {
                maxDaysLastMonth = DicoMonthAndMaxDay.MonthMaxDay.Where(x => x.Key == d.Month - 1).FirstOrDefault().Value;

                //Set maxDaysLastMonth to 29 if leap year and last month is February
                if (DateTime.IsLeapYear(d.Year) && d.Month == 3)
                {
                    maxDaysLastMonth = 29;

                }
            }

            //cast to get the number from the enum DayOfweek see DayOfWeekSearch for more infos
            int dow = (int)DayOfWeekSearch(d);

            // if dow is sunday set it to 7 not 0 to prevent skipping the next loop
            if (dow == 0)
            {
                dow = 7;
            }
            // set dayofweek in the customday
            int count = 0;


            //Fill the first week of the customMonth with the last days from the last month(e.g. mon26, tue27, wed28...)
            //(Exemple March 2018)
            //Loop 1
            for (int i = dow; i > 1; i--)
            {


                // +2 because we want the last itteration assign maxDaysLastMonth to cd.DayInMonth
                dayInMonth = maxDaysLastMonth - i + 2;
                // add cd.month to get the month in the view (ComboBox event Date)
                month = d.Month-1;

                year = d.Year;

                if (month == 0)
                {
                    month = 12;
                    year = d.Year - 1;
                }

                CustomDay cd = new CustomDay(dayInMonth, month, year);

                cd.DayInWeek = count + 1;

                // add customday in the right column
                cm.Days.Add(cd);

                count++;

                allCreatedDays++;
            }

            //Represents the day of the real month (similar to "Day" in the DateTime class)
            int currentDay = 1;

            //Fill the customMonth starting where the "Loop 1" ended until the end of the real month (e.g. thu1, fri2... => fri30, sat31)
            //Loop 2
            for (int i = dow; i <= (maxDays+dow)-1; i++)
            {
                
                int resultDow = i % 7;

                if (resultDow == 0)
                    resultDow = 7;

                month = d.Month;

                year = d.Year;

                dayInMonth = currentDay;

                CustomDay cd = new CustomDay(dayInMonth, month, year);

                cd.DayInWeek = resultDow;

                cm.Days.Add(cd);

                currentDay++;

                allCreatedDays++;
            }

            //Reset count to assign dayInWeek to all remaining customsDays
            count = 1;

            //Fill the rest of the custom month (until 42 customDays created)
            //Loop 3
            for (int i = allCreatedDays; i < maxcustomDays; i++)
            {

                int resultDow = allCreatedDays + 1 % 7;

                if (resultDow == 0)
                    resultDow = 7;

                year = d.Year;

                month = d.Month + 1;
                if (month == 13)
                {
                    month = 1;
                    year = d.Year + 1;
                }

                dayInMonth = count;

                CustomDay cd = new CustomDay(dayInMonth, month, year);

                cd.DayInWeek = resultDow;

                cm.Days.Add(cd);

                count++;

                currentDay++;

                allCreatedDays++;
            }           

            return cm;
        }



        //get the enum of the first day of the month from a given datetime
        public static DayOfWeek DayOfWeekSearch(DateTime d)
        {
            DateTime dt = new DateTime(d.Year, d.Month, 1);

            DayOfWeek dow = dt.DayOfWeek;

            return dow;
        }
    }
}
