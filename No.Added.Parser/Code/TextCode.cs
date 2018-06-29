﻿namespace No.Added.Parser.Code
{
    using Nodes;
    using Expressions;

    public class TextCode : TokenCode
    {
        public override Node ParseLiteral(DefaultParser parser)
        {
            if (!this.Next.EndOfExpression)
            {
                throw parser.Error("Literal is not end node.");
            }

            return new StringLiteral(parser, this);
        }

        public override string ParseString(DefaultParser parser)
        {
            return this.Text;
        }

        internal override void ListToken(DefaultParser parser, int separator, ListNode list, TokenCode left, TokenCode right, TokenCode end)
        {
            this.Next.ListToken(parser, separator, list, left, this, end);
        }

    }
}
