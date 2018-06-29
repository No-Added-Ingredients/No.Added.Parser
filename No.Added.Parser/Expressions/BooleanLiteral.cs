namespace No.Added.Parser.Expressions
{
    using Nodes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Code;

    public class BooleanLiteral : EndNode<bool>
    {
        public BooleanLiteral(DefaultParser parser, TokenCode code) : base(parser, code)
        {
            this.Type = NodeType.Boolean;
        }

        public override void Accept(IVisitor visitor, Node context = null)
        {
            visitor.Visit(this, context);
        }

        protected override bool Initialize(DefaultParser parser, TokenCode code)
        {
            return code.ParseBoolean(parser);
        }
    }
}
