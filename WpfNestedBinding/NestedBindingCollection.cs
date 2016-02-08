using System;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace WpfNestedBinding
{
    public class NestedBindingCollection : Collection<BindingBase>
    {
        protected override void InsertItem(int index, BindingBase item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            ValidateItem(item);

            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, BindingBase item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            ValidateItem(item);

            base.SetItem(index, item);
        }

        private void ValidateItem(BindingBase binding)
        {
            if (binding == null)
                throw new ArgumentNullException(nameof(binding));
        }
    }
}
