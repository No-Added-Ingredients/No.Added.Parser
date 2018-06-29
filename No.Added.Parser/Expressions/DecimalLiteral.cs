﻿namespace No.Added.Parser.Expressions
{
    using Nodes;
    using Code;

    public class DecimalLiteral : EndNode<double>
    {
        public DecimalLiteral(DefaultParser parser, TokenCode code) : base(parser, code)
        {
        }

        public override void Accept(IVisitor visitor, Node context = null)
        {
            visitor.Visit(this, context);
        }

        protected override double Initialize(DefaultParser parser, TokenCode code)
        {
            return code.ParseDouble(parser);
        }
    }
}
