using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public class UniqueGroupOfNames<T> where T: INamedItem
    {
        public virtual bool AddItem(T item)
        {
            if(Items.SingleOrDefault(T=>T.Name==item.Name) != null )
            {
                return false;
            }
            _subscriptions[item] = item.OnNameChanging.Subscribe(ItemNameChanging);
            _items.Add(item);
            return true;
        }

        readonly Dictionary<T, IDisposable> _subscriptions = new Dictionary<T,IDisposable>();
        public bool RemoveItem(T item)
        {
            if (Items.SingleOrDefault(T => T.Name == item.Name) == null)
            {
                return false;
            }
            _subscriptions[item].Dispose();
            _items.Remove(item);
            return true;
        }

        void ItemNameChanging(BeforePropertyChanged<INamedItem> beforePropertyChanged)
        {
            if(Items.SingleOrDefault(T=>T.Name==(string)beforePropertyChanged.NewPropertyValue) != null )
            {
                beforePropertyChanged.Cancel = true;
            }
        }

        private readonly List<T> _items = new List<T>();

        public IEnumerable<T> Items
        {
            get { return _items; }
        }
    }
}
