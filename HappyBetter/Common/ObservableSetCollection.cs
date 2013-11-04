﻿using System;
using System.Collections.ObjectModel;

namespace HappyBetter.Common
{
    public class ObservableSetCollection<T> : ObservableCollection<T>
    {
        protected override void InsertItem(int index, T item)
        {
            if (Contains(item)) throw new Exception("Item already exists in the set.");

            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, T item)
        {
            int i = IndexOf(item);
            if (i >= 0 && i != index) throw new Exception("Item already exists in the set.");

            base.SetItem(index, item);
        }
    }
}
