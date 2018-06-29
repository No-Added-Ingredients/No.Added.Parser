namespace No.Added.Parser.Expressions
{
    using System;
    using Nodes;
    using Code;

    public class ElementList : ListNode
    {
        public override void Accept(IVisitor visitor, Node context = null)
        {
            visitor.Visit(this, context);
        }

        protected override Node InitializeNode(DefaultParser parser, TokenCode code, int index)
        {
            throw new NotImplementedException();
        }
    }
}
