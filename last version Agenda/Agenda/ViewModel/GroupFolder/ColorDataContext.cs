using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Agenda.ViewModel.GroupFolder
{
    public class ColorDataContext
    {
        public string ColorName { get; set; }
        public SolidColorBrush ColorPreset { get; set; }
        public ColorDataContext DataContext
        {
            get { return this; }
        }

        public ColorDataContext(string name, SolidColorBrush color)
        {
            ColorName = name;
            ColorPreset = color;
        }
    }
}
