namespace No.Added.Parser.Code
{
    using System;
    using Nodes;
    using Expressions;

    public class TokenCode : RawCode
    {
        public string Text
        {
            get;

            internal set;
        }

        public bool EndOfExpression { get; protected set; } = false;

        public NodeType Type { get; internal set; } = NodeType.Unknown;

        public virtual TokenCode Next
        {
            get;

            set;
        }

        public override T ParseExpression<T>(DefaultParser parser, Func<TokenCode, TokenCode, T> tokenOf)
        {
            return this.Next.ExpressionToken(parser, this, this, tokenOf);
        }

        public override Node ParseLiteral(DefaultParser parser)
        {
            return this.Next.ParseLiteral(parser);
        }

        public override int ParseInt32(DefaultParser parser)
        {
            throw parser.Error(string.Format("This is not a Int32: {0}.", this.Text));
        }

        public override double ParseDouble(DefaultParser parser)
        {
            throw parser.Error(string.Format("This is not a Double: {0}.", this.Text));
        }

        public override bool ParseBoolean(DefaultParser parser)
        {
            throw parser.Error(string.Format("This is not a Boolean: {0}.", this.Text));
        }

        public override string ParseString(DefaultParser parser)
        {
            throw parser.Error(string.Format("This is not a String: {0}.", this.Text));
        }

        public override GroupExpression ParseGroup(DefaultParser parser)
        {
            return null;
        }

        public override void ParseList(DefaultParser parser, int separator, ListNode list, TokenCode end)
        {
            this.Next.ListToken(parser, separator, list, this, this, end);
        }

        public override CallExpression ParseCall(DefaultParser parser)
        {
            return null;
        }

        internal virtual T ExpressionToken<T>(DefaultParser parser, TokenCode startLeft, TokenCode endLeft, Func<TokenCode, TokenCode, T> tokenOf) where T : Nodes.Node
        {
            endLeft.Next = Utils.CodeSplit;
            var result = tokenOf(startLeft, this);
            if (result == null)
            {
                endLeft.Next = this;
                return this.Next.ExpressionToken(parser, startLeft, this, tokenOf);
            }

            return result;
        }

        internal virtual void ListToken(DefaultParser parser, int separator, ListNode list, TokenCode left, TokenCode right, TokenCode end)
        {
            if (this.Text.Length == 1 && this.Text[0] == Convert.ToChar(separator))
            {
                right.Next = Utils.CodeSplit;
                list.Add(parser, left);
                this.Next.ParseList(parser, separator, list, end);
                return;
            }

            this.Next.ListToken(parser, separator, list, left, this, end);
        }
    }
}
