namespace No.Added.Parser.Expressions
{
    using Nodes;
    using Code;

    public class StringLiteral : EndNode<string>
    {
        public StringLiteral(DefaultParser parser, TokenCode code) : base(parser, code)
        {
            this.Type = NodeType.String;
        }

        public override void Accept(IVisitor visitor, Node context = null)
        {
            visitor.Visit(this, context);
        }

        protected override string Initialize(DefaultParser parser, TokenCode code)
        {
            return code.ParseString(parser);
        }
    }
}
