using Agenda.Resources.Styles;
using Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Agenda.ViewModel.EventFolder
{
    public class GroupDataContext
    {
        public Group Group { get; set; }
        public string Name
        {
            get
            {
                return Group.Name;
            }
        }
        public SolidColorBrush GroupColor
        {
            get
            {
                return  new SolidColorBrush(CustomColors.FromString(Group.RGBAColor));
            }
        }
        public GroupDataContext DataContext
        {
            get { return this; }
        }
        public GroupDataContext(Group group)
        {
            Group = group;
        }
    }
}
