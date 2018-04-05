using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolboxMvvm.wpf
{
    public abstract class ViewModelCollectionBase<T> : ViewModelBase where T : class
    {
        private ObservableCollection<T> items;

        public ObservableCollection<T> Items
        {
            get { return items = items ?? LoadItems(); }
            protected set
            {
                if (items != value)
                {
                    items = value;
                    RaisePropertyChanged(nameof(Items));
                }
            }
        }

        public abstract ObservableCollection<T> LoadItems();


        private T selectedItem;

        public T SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    RaisePropertyChanged(nameof(SelectedItem));
                }
            }
        }
    }
}
