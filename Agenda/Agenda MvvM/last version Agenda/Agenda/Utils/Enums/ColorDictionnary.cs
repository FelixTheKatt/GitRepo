using Agenda.Resources.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Agenda.Utils.Enums
{
    public static class ColorDictionnary
    {
        public static readonly Dictionary<string, Color> Colors
        = new Dictionary<string, Color>
         {
             { "GroupBarbiePink", CustomColors.GroupBarbiePink },
             { "GroupBlue", CustomColors.GroupBlue },
             { "GroupBrown", CustomColors.GroupBrown },
             { "GroupGreen", CustomColors.GroupGreen },
             { "GroupOrange", CustomColors.GroupOrange },
             { "GroupPink", CustomColors.GroupPink },
             { "GroupPurple", CustomColors.GroupPurple },
             { "GroupRed", CustomColors.GroupRed },
             { "GroupYellow", CustomColors.GroupYellow }
         };
    }
}
