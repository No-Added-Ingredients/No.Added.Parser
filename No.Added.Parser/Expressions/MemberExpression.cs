namespace No.Added.Parser.Expressions
{
    using Nodes;
    using Code;
    using System.Collections.Generic;
    using System;

    public class MemberExpression : TreeNode, IPathExpression
    {
        private string[] path = null;

        public MemberExpression(DefaultParser parser, TokenCode left, NodeType nodeType, TokenCode right) : base(parser, left, nodeType, right)
        {
        }

        public string[] Path
        {
            get
            {
                if (this.path == null)
                {
                    var result = new List<string>();
                    this.AddTo(result);
                    this.path = result.ToArray();
                }

                return this.path;
            }
        }

        public override void Accept(IVisitor visitor, Node context = null)
        {
            visitor.Visit(this, context);
            this.Left.Accept(visitor, this);
            this.Right.Accept(visitor, this);
        }

        public void AddTo(List<string> path)
        {
            (this.Left as IPathExpression).AddTo(path);
            (this.Right as IPathExpression).AddTo(path);
        }

        protected override Node InitializeLeft(DefaultParser parser, TokenCode code)
        {
            Node node = parser.TryCall(code);
            if (node != null)
            {
                this.LeftNode = node;
                return node;
            }

            node = parser.TryLiteral(code);
            if (node != null)
            {
                this.LeftNode = node;
                return node;
            }

            throw parser.Error("Member expression expected");
        }

        protected override Node InitializeRight(DefaultParser parser, TokenCode code)
        {
            Node node = parser.TryMember(code);
            if (node != null)
            {
                this.RightNode = node;
                return node;
            }

            node = parser.TryCall(code);
            if (node != null)
            {
                this.RightNode = node;
                return node;
            }

            node = parser.TryIdentifier(code);
            if (node != null)
            {
                this.RightNode = node;
                return node;
            }

            throw parser.Error("Identifier expected");
        }
    }
}
