using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WpfNestedBinding
{
    internal class NestedBindingsConverter : IMultiValueConverter
    {
        public NestedBindingsConverter(NestedBindingsTree tree, Type targetType)
        {
            Tree = tree;
            TargetType = targetType;
        }

        private NestedBindingsTree Tree { get; }

        private Type TargetType { get; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = GetTreeValue(Tree, values);
            return value;
        }

        private object GetTreeValue(NestedBindingsTree tree, object[] values)
        {
            var objects = tree.Nodes.Select(x => x is NestedBindingsTree ? GetTreeValue((NestedBindingsTree)x, values) : values[x.Index]).ToArray();
            var value = tree.Converter.Convert(objects, TargetType, tree.ConverterParameter, tree.ConverterCulture);
            return value;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
