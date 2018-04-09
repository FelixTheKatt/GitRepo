using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Utils.Enums
{
    public static class WarningUtils
    {
        public static readonly Dictionary<string, string> ErrorCodes
        = new Dictionary<string, string>
         {
             { "NoSpace", "This field can't contain spaces" },
             { "AlreadyExist", "Already Exist" } ,
             { "EmailInvalid", "This email is invalid" } ,
             { "Null", "This field is required" } ,
             { "DontExist" , "The pseudo or password is invalid" },
             { "TimeUnchecked" , "Time won't be saved" },
             {"NoFind", "Nothing was Found :(" }
         };

    }
}
