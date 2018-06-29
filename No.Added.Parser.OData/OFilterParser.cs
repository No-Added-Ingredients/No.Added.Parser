using No.Added.Parser;
using No.Added.Parser.Code;
using No.Added.Parser.Expressions;
using No.Added.Parser.Nodes;
using No.Added.Parser.Readers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No.Added.Parser.OData
{
    public class OFilterParser: DefaultParser
    {
        protected override TokenCode ParseExponentPart(ScriptReader stream)
        {
            var code = stream.Read(this);
            if (!(code == Utils.MinusSign || code == Utils.PlusSign))
            {
                throw this.Error("Expect '+' or '-'");
            }

            for (code = stream.Read(this); code != -1; code = stream.Read(this))
            {
                if (code >= Utils.ZeroDigit && code <= Utils.NineDigit)
                {
                    continue;
                }

                break;
            }

            if (stream.Position < 4)
            {
                throw this.Error("Unexpected end of exponent part.");
            }

            var text = String.Empty;
            if (code == Utils.SmallD)
            {
                text = stream.SplitAt(stream.Position);
                text = text.Substring(0, text.Length - 1);
            }
            else
            {
                text = stream.SplitAt(stream.Position - 1);
            }


            return new NumberCode(text, NodeType.ExponentDecimal);
        }

        protected override TokenCode ParseDecimalCode(NodeType offsetType, ScriptReader stream)
        {
            var type = offsetType;
            for (var code = stream.Read(this); code != -1; code = stream.Read(this))
            {
                if (code >= Utils.ZeroDigit && code <= Utils.NineDigit)
                {
                    continue;
                }

                switch (code)
                {
                    case Utils.CapitalE:
                    case Utils.SmallE:
                        return this.ParseExponentPart(stream);

                    case Utils.SmallF:      // Single
                    case Utils.SmallD:      // Double
                    case Utils.SmallM:      // Decimal
                    case Utils.CapitalM:    // Decimal
                        var decimalValue = stream.SplitAt(stream.Position);
                        return new NumberCode(decimalValue.Substring(0, decimalValue.Length - 1), NodeType.Decimal);

                    case Utils.CapitalL: // Large int
                        if (type == NodeType.Decimal)
                        {
                            throw this.Error("Unexpected 'L'");
                        }

                        var integerValue = stream.SplitAt(stream.Position);
                        return new NumberCode(integerValue.Substring(0, integerValue.Length - 1), NodeType.Integer);

                    case Utils.FullStop:
                        if (type == NodeType.Decimal)
                        {
                            throw this.Error("Unexpected '.'");
                        }

                        type = NodeType.Decimal;
                        continue;
                }

                break;
            }

            return new NumberCode(stream.SplitAt(stream.Position - 1), type);
        }

        public override bool IsNameSeparator(int code)
        {
            // Do not use before IsStartNumber
            return code == Utils.Solidus;
        }

        protected override void Initialize()
        {
            //this.separateToken = token =>
            //{
            //    switch (token)
            //    {
            //        case '"':
            //            return NodeType.SingleQuote;

            //        case '\'':
            //            return NodeType.DoubleQuote;

            //        case '(':
            //            return NodeType.RoundOpenBracket;

            //        default:
            //            return NodeType.Unknown;
            //    }
            //};

            this.tokenOfRoot = code =>
            {
                switch (code)
                {
                    case "not":
                        return NodeType.Not;

                    case "+":
                        return NodeType.Plus;

                    case "-":
                        return NodeType.Minus;

                    default:
                        return NodeType.Unknown;
                }
            };

            this.tokenOfTree = code =>
            {
                switch (code)
                {
                    case "\"":
                        return NodeType.DoubleQuote;

                    case "'":
                        return NodeType.SingleQuote;

                    case "add":
                        return NodeType.Addition;

                    case "sub":
                        return NodeType.Subtraction;

                    case "mul":
                        return NodeType.Multiplication;

                    case "div":
                        return NodeType.Division;

                    case "mod":
                        return NodeType.Modulus;

                    case "eq":
                        return NodeType.Equal;

                    case "ne":
                        return NodeType.NotEqual;

                    case "gt":
                        return NodeType.GreaterThan;

                    case "ge":
                        return NodeType.GreaterThanOrEqual;

                    case "lt":
                        return NodeType.LessThan;

                    case "le":
                        return NodeType.LessThanOrEqual;

                    case "or":
                        return NodeType.LogicalOR;

                    case "and":
                        return NodeType.LogicalAND;

                    case "/":
                        return NodeType.NameSeparator;

                    default:
                        return NodeType.Unknown;
                }
            };

        }

        protected override NodeType ReservedWordCheck(string word)
        {
            switch (word)
            {
                case "datetime":
                    return NodeType.DateTime;

                default:
                    return base.ReservedWordCheck(word);
            }
        }

        public override Node ParseLiteral(IdentifierCode code)
        {
            switch (code.Type)
            {
                case NodeType.DateTime:
                    return new StringLiteral(this, code.Next);

                default:
                    return base.ParseLiteral(code);
            }
        }
    }
}
