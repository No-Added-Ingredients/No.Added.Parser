namespace No.Added.Parser.Code
{
    using Nodes;
    using Expressions;

    public class IdentifierCode : TokenCode
    {

        
        public override Node ParseLiteral(DefaultParser parser)
        {
            return parser.ParseLiteral(this);
        }
 
        public override CallExpression ParseCall(DefaultParser parser)
        {
            if (this.Next.Type == NodeType.Group)
            {
                var next = this.Next;
                this.Next = Utils.CodeSplit;
                return new CallExpression(parser, this, NodeType.Function, next);
            }

            return null;
        }

        public override bool ParseBoolean(DefaultParser parser)
        {
            return parser.IsTrue(this.Text);
        }

        internal override void ListToken(DefaultParser parser, int separator, ListNode list, TokenCode left, TokenCode right, TokenCode end)
        {
            this.Next.ListToken(parser, separator, list, left, this, end);
        }
    }
}
