using Agenda.Resources.Styles;
using Agenda.ViewModel.GroupFolder;
using Agenda.Windows.Group;
using Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using ToolboxMvvm.wpf;

namespace Agenda.ViewModel.AgendaFolder
{
    public class GroupDataContext
    {
        public Group Group { get; set; }
        public SolidColorBrush GroupColor
        {
            get
            {
                return new SolidColorBrush(CustomColors.FromString(Group.RGBAColor));

            }
        }
        public string Name
        {
            get
            {
                return Group.Name;
            }
        }
        public int NumberOfEvent { get; set; }

        public GroupDataContext()
        {

        }
        public GroupDataContext(Group group)
        {
            this.Group = group;
        }

        public GroupDataContext DataContext
        {
            get { return this; }
        }

        private ICommand viewDetailsCommand;

        public ICommand ViewDetailsCommand
        {
            get
            {
                return viewDetailsCommand = viewDetailsCommand ?? new CommandBase(ViewDetails);
            }
        }

        private void ViewDetails()
        {
            GroupDetailsViewModel gdvm = new GroupDetailsViewModel();
            gdvm.Group = Group;

            GroupDetailsWindow window = new GroupDetailsWindow()
            {
                DataContext = gdvm
            };

            window.ShowDialog();
        }
    }
}
