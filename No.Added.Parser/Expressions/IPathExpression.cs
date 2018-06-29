namespace No.Added.Parser.Expressions
{
    using System.Collections.Generic;

    public interface IPathExpression
    {
        void AddTo(List<string> path);

        string[] Path
        {
            get;
        }
    }
}
