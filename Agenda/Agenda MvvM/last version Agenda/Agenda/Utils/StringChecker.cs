using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Agenda.Utils
{
    public static class StringChecker
    {


        public static bool HasSpaces(string word)
        {
            if (word == null)
            {
                return false;
            }

            List<char> charList = word.ToList();

            foreach (char c in charList)
            {
                if (c == ' ')
                {
                    return true;
                }
            }

            return false;
        }


        public static bool IsValidEmail(string email)
        {

            // Needed because the updateSourceTrigger from the binding go into this method every microseconds 
            // and trigger an NullReferenceException from "Regex.IsMatch"
            if (email == null)
                return false;

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
