using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfNestedBinding
{
    [ContentProperty(nameof(Bindings))]
    public class NestedBinding : MarkupExtension, IAddChild
    {
        public NestedBinding()
        {
            Bindings = new NestedBindingCollection();
        }

        public NestedBindingCollection Bindings { get; }

        public IMultiValueConverter Converter { get; set; }

        public object ConverterParameter { get; set; }

        public CultureInfo ConverterCulture { get; set; }

        public MultiBinding MultiBinding { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (MultiBinding != null)
            {
                if (Bindings.Any())
                    throw new ArgumentException($"{nameof(Bindings)} should be an empty");
                if (Converter != null)
                    throw new ArgumentException($"{nameof(Converter)} should be a null");
            }
            else
            {
                if (!Bindings.Any())
                    throw new ArgumentNullException(nameof(Bindings));
                if (Converter == null)
                    throw new ArgumentNullException(nameof(Converter));
            }

            var target = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            if (target.TargetObject.GetType().FullName == "System.Windows.SharedDp")
                return this;

            if (MultiBinding != null)
            {
                Converter = MultiBinding.Converter;
                ConverterParameter = MultiBinding.ConverterParameter;
                ConverterCulture = MultiBinding.ConverterCulture;
                foreach (var bindingBase in MultiBinding.Bindings)
                    Bindings.Add(bindingBase);
            }

            if (target.TargetObject is NestedBindingCollection)
            {
                var binding = new Binding
                {
                    Source = this
                };
                return binding;
            }

            var multiBinding = new MultiBinding
            {
                Mode = BindingMode.OneWay
            };
            var tree = GetNestedBindingsTree(this, multiBinding);
            var converter = new NestedBindingConverter(tree);
            multiBinding.Converter = converter;

            return multiBinding.ProvideValue(serviceProvider);
        }

        private static NestedBindingsTree GetNestedBindingsTree(NestedBinding nestedBinding, MultiBinding multiBinding)
        {
            var tree = new NestedBindingsTree
            {
                Converter = nestedBinding.Converter,
                ConverterParameter = nestedBinding.ConverterParameter,
                ConverterCulture = nestedBinding.ConverterCulture
            };
            foreach (var bindingBase in nestedBinding.Bindings)
            {
                var binding = bindingBase as Binding;
                var childNestedBinding = binding?.Source as NestedBinding;
                if (childNestedBinding != null && binding.Converter == null)
                {
                    tree.Nodes.Add(GetNestedBindingsTree(childNestedBinding, multiBinding));
                    continue;
                }

                tree.Nodes.Add(new NestedBindingNode(multiBinding.Bindings.Count));
                multiBinding.Bindings.Add(bindingBase);
            }

            return tree;
        }

        public void AddChild(object value)
        {
            var binding = value as BindingBase;
            if (binding != null)
                Bindings.Add(binding);
            else
                throw new ArgumentNullException(nameof(value));
        }

        public void AddText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            if (!string.IsNullOrWhiteSpace(text))
                throw new ArgumentException();
        }
    }
}
