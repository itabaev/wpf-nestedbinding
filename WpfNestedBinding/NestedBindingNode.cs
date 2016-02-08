namespace WpfNestedBinding
{
    internal class NestedBindingNode
    {
        public NestedBindingNode(int index)
        {
            Index = index;
        }

        public int Index { get; }

        public override string ToString()
        {
            return Index.ToString();
        }
    }
}
