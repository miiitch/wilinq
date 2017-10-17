using System;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class BooleanOperationStatement : WhereStatement
    {
        private const string STR_AND = "And";
        private const string STR_OR = "Or";

        /// <summary>
        ///     Initializes a new instance of the BooleanOperationStatement class.
        /// </summary>
        public BooleanOperationStatement(WhereStatement left, BooleanOperationStatementType type, WhereStatement right)
        {
            Left = left;
            Right = right;
            Type = type;
        }

        public WhereStatement Left { get; }
        public WhereStatement Right { get; }
        public BooleanOperationStatementType Type { get; }


        protected internal override string ConvertToQueryValue()
        {
            string op;


            switch (Type)
            {
                case BooleanOperationStatementType.And:
                    op = STR_AND;
                    break;
                case BooleanOperationStatementType.Or:
                    op = STR_OR;
                    break;
                default:
                    throw new NotSupportedException();
            }

            return $"({Left.ConvertToQueryValue()} {op} {Right.ConvertToQueryValue()})";
        }
    }
}