namespace No.Added.Parser.Expressions
{
    using Nodes;
    using Code;
    using System;
    using System.Collections.Generic;

    public class Identifier : EndNode<string>, IPathExpression
    {
        private string[] path = null;

        public Identifier(DefaultParser parser, TokenCode code) : base(parser, code)
        {
            this.Type = NodeType.IdentifierName;
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
        }

        public void AddTo(List<string> path)
        {
            path.Add(this.Value);
        }

        protected override string Initialize(DefaultParser parser, TokenCode code)
        {
            return code.Text;
        }
    }
}
