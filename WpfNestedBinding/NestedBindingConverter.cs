using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WpfNestedBinding
{
    internal class NestedBindingConverter : IMultiValueConverter
    {
        public NestedBindingConverter(NestedBindingsTree tree)
        {
            Tree = tree;
        }

        private NestedBindingsTree Tree { get; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = GetTreeValue(Tree, values, targetType, culture);
            return value;
        }

        private object GetTreeValue(NestedBindingsTree tree, object[] values, Type targetType, CultureInfo culture)
        {
            var objects = tree.Nodes.Select(x => x is NestedBindingsTree ? GetTreeValue((NestedBindingsTree)x, values, targetType, culture) : values[x.Index]).ToArray();
            var value = tree.Converter.Convert(objects, targetType, tree.ConverterParameter, tree.ConverterCulture ?? culture);
            return value;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
