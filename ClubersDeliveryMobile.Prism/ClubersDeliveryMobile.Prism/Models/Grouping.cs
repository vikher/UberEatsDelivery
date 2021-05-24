using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ClubersDeliveryMobile.Prism.Models
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        private DateTime key;
        private IGrouping<DateTime, Transaction> transGroup;

        public DateTime Key { get; private set; }

        public Grouping(DateTime key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }

    }
}
