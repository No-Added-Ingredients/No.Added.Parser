using No.Added.Parser.Nodes;

namespace No.Added.Parser.Expressions
{
    public interface IVisitor
    {
        void Visit(MemberExpression node, Node context);

        void Visit(GroupExpression node, Node context);

        void Visit(StringLiteral node, Node context);

        void Visit(DecimalLiteral node, Node context);

        void Visit(BooleanLiteral node, Node context);

        void Visit(LogicalExpression node, Node context);

        void Visit(AdditiveExpression node, Node context);

        void Visit(MultiplicativeExpression node, Node context);

        void Visit(EqualityExpression node, Node context);

        void Visit(RelationalExpression node, Node context);

        void Visit(UnaryExpression node, Node context);

        void Visit(CallExpression node, Node context);

        void Visit(Identifier node, Node context);

        void Visit(ArgumentList node, Node context);

        void Visit(ElementList node, Node context);

        void Visit(IntegerLiteral node, Node context);
    }
}
