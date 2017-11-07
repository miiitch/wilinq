using System;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class FieldOperationStatement : WhereStatement
    {
        /// <summary>
        ///     Initializes a new instance of the FieldOperationStatement class.
        /// </summary>
        public FieldOperationStatement(FieldStatement field, FieldOperationStatementType type, ValueStatement rightValue)
        {
  
            Field = field;
            Type = type;
            RightStatement = rightValue;
         
        }

        public FieldOperationStatement(FieldStatement field, FieldOperationStatementType type, FieldStatement rightField)
        {

            Field = field;
            Type = type;
            RightStatement = rightField;

        }

        public FieldStatement Field { get; }
        public FieldOperationStatementType Type { get; }
        public Statement RightStatement { get; }


        
        

        protected internal override string ConvertToQueryValue()
        {                   
            string op;

            switch (Type)
            {
                case FieldOperationStatementType.Equals:
                    op = "=";
                    break;
                case FieldOperationStatementType.NotContains:
                    op = "not contains";
                    break;
                case FieldOperationStatementType.Contains:
                    op = "contains";
                    break;
                case FieldOperationStatementType.IsGreater:
                    op = ">";
                    break;
                case FieldOperationStatementType.IsGreaterOrEqual:
                    op = ">=";
                    break;
                case FieldOperationStatementType.IsLess:
                    op = "<";
                    break;
                case FieldOperationStatementType.IsLessOrEqual:
                    op = "<=";
                    break;
                case FieldOperationStatementType.IsIn:
                    op = "in";
                    break;
                case FieldOperationStatementType.IsInGroup:
                    op = "in group";
                    break;
                case FieldOperationStatementType.IsNotInGroup:
                    op = "not in group";
                    break;
                case FieldOperationStatementType.IsDifferent:
                    op = "<>";
                    break;
                case FieldOperationStatementType.IsUnder:
                    op = "under";
                    break;
                case FieldOperationStatementType.IsNotUnder:
                    op = "not under";
                    break;
                case FieldOperationStatementType.WasEver:
                    op = "ever";
                    break;
                default:
                    throw new NotImplementedException();
            }

            return $"{Field.ConvertToQueryValue()} {op} {RightStatement.ConvertToQueryValue()}";
        }
    }
}