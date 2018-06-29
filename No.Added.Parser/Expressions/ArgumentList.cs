namespace No.Added.Parser.Expressions
{
    using Nodes;
    using Code;

    public class ArgumentList : ListNode
    {
        public override void Accept(IVisitor visitor, Node context = null)
        {
            visitor.Visit(this, context);
            for (var index = 0; index < this.Nodes.Count; index++)
            {
                Nodes[index].MySelf().Accept(visitor, this);
            }
        }

        protected override Node InitializeNode(DefaultParser parser, TokenCode code, int index)
        {
            var node = parser.Parse(code);
            if (node != null)
            {
                this.Nodes[index] = node;
                return node;
            }

            throw parser.Error("Invalid argument at index " + index);
        }
    }
}
