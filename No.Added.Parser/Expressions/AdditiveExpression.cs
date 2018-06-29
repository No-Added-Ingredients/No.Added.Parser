namespace No.Added.Parser.Expressions
{
    using Nodes;
    using Code;

    public class AdditiveExpression : MultiplicativeExpression
    {
        public AdditiveExpression(DefaultParser parser, TokenCode left, NodeType nodeType, TokenCode right) : base(parser, left, nodeType, right)
        {
        }

        public override void Accept(IVisitor visitor, Node context = null)
        {
            visitor.Visit(this, context);
            this.Left.Accept(visitor, this);
            this.Right.Accept(visitor, this);
        }

        protected override Node Initialize(DefaultParser parser, TokenCode code)
        {
            Node node = parser.TryAdditive(code);
            if (node != null)
            {
                return node;
            }

            return base.Initialize(parser, code);
        }
    }
}
