using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Agenda.Resources.Styles
{

    //Custom colors for use in other classes (unable to use "Color.xaml" in other classes)
    //Inspired from the "Color" and "Colors" classes in the "Presentation Core" Assembly
    public static class CustomColors
    {

        //Stolen from c# itself
        //We don't know how to use it
        private static Color FromUInt32(uint argb)
        {
            Color color = new Color();
            color.A = (byte)((argb & 4278190080U) >> 24);
            color.R = (byte)((argb & 16711680U) >> 16);
            color.G = (byte)((argb & 65280U) >> 8);
            color.B = (byte)(argb & (uint)byte.MaxValue);

            return color;
        }

        //We don't use this because we don't know which values to use
        //public static Color Selected { get { return FromUInt32(4200000000U); } }


        //Create a color from four bytes(value between 0 and 255) used for "A", "R", "G", "B" (Alpha, Red, Green, Blue)
        public static Color FromFourByte(Byte a, Byte r, Byte g, Byte b)
        {
            Color color = new Color();
            color.A = a;
            color.R = r;
            color.G = g;
            color.B = b;

            return color;
        }

        public static Color FromString(string argb)
        {
            Color color = new Color();
            string a = argb.Substring(0, 3); 
            string r = argb.Substring(3, 3); 
            string g = argb.Substring(6, 3); 
            string b = argb.Substring(9, 3);

            color.A = (Byte.Parse(a));
            color.R = (Byte.Parse(r));
            color.G = (Byte.Parse(g));
            color.B = (Byte.Parse(b));
            return color;
        }

        public static string ColorToString(Color color)
        {
            string result;

            string a = color.A.ToString();

            while (a.Length < 3)
            {
                a = "0" + a;
            }

            result = a;

            string r = color.R.ToString();

            while (r.Length < 3)
            {
                r = "0" + r;
            }

            result += r;

            string g = color.G.ToString();

            while (g.Length < 3)
            {
                g = "0" + g;
            }

            result += g;

            string b = color.B.ToString();

            while (b.Length < 3)
            {
                b = "0" + b;
            }

            result += b;

            return result;
        }

        //public static StringToColot (string color)
        //{

        //}

        //Create our custom color
        public static Color DefaultSelected { get { return FromFourByte(200, 99, 196, 255); } }
        public static Color Selected { get { return FromFourByte(200, 50, 255, 30); } }
        public static Color Warning { get { return FromFourByte(255, 185, 52, 45); } }
        public static Color Info { get { return FromFourByte(255, 30, 145, 19); } }



        //GroupColors
        public static Color GroupRed { get { return FromFourByte(255, 255, 000, 000); } }
        public static Color GroupGreen { get { return FromFourByte(255, 000, 255, 000); } }
        public static Color GroupBlue { get { return FromFourByte(255, 000, 000, 255); } }
        public static Color GroupYellow { get { return FromFourByte(255, 241, 244, 046); } }
        public static Color GroupBrown { get { return FromFourByte(255, 094, 096, 064); } }
        public static Color GroupPurple { get { return FromFourByte(255, 094, 045, 150); } }
        public static Color GroupOrange { get { return FromFourByte(255, 255, 102, 000); } }
        public static Color GroupPink { get { return FromFourByte(255, 224, 069, 162); } }
        public static Color GroupBarbiePink { get { return FromFourByte(255, 255, 000, 152); } }




        //Used for test pruposes
        public static Color Test { get { return FromFourByte(0, 0, 0, 0); } }

        public static Color CustomRed { get { return FromFourByte(255, 255, 0, 0); } }



    }
}
