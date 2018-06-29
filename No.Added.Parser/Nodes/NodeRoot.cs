namespace No.Added.Parser.Nodes
{
    using Expressions;

    public abstract class NodeRoot<T> : Node where T : Node
    {
        private Node root = null;

        public NodeRoot(string text)
        {
            this.root = this.Initialize(this.CreateParser(), text);
        }

        public T Root
        {
            get
            {
                return this.root.MySelf() as T;
            }
        }

        public override void Accept(IVisitor visitor, Node context = null)
        {
            this.Root.Accept(visitor, this);
        }

        protected abstract DefaultParser CreateParser();

        protected abstract ProxyNode Initialize(DefaultParser parser, string text);
    }
}
