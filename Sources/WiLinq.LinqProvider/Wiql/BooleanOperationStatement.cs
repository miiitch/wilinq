using System;
using System.Collections.Generic;
using System.Linq;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed  class BooleanOperationStatement : WhereStatement
    {
        private const string STR_And = "And";
        private const string STR_Or = "Or";
        public WhereStatement Left {  get; private set; }
        public WhereStatement Right { get; private set; }
        public BooleanOperationStatementType Type {  get; private set; }

        /// <summary>
        /// Initializes a new instance of the BooleanOperationStatement class.
        /// </summary>
        public BooleanOperationStatement(WhereStatement left, BooleanOperationStatementType type, WhereStatement right)
        {
            Left = left;
            Right = right;
            Type = type;
        }


        protected internal override string ConvertToQueryValue()
        {
            string op;


            switch (Type)
            {
                case BooleanOperationStatementType.And:
                    op = STR_And;
                    break;
                case BooleanOperationStatementType.Or:
                    op = STR_Or;
                    break;
                default:
                    throw new NotSupportedException();
            }

            return string.Format("({0} {1} {2})", Left.ConvertToQueryValue(), op, Right.ConvertToQueryValue());
        }
        
    }
}
