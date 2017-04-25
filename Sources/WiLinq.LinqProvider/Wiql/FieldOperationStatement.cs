using System;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class FieldOperationStatement : WhereStatement
    {
        public string Field { get; private set; }
        public FieldOperationStatementType Type { get; private set; }
        public ValueStatement Value { get; private set; }
        public FieldType FieldType { get; private set; }

        /// <summary>
        /// Initializes a new instance of the FieldOperationStatement class.
        /// </summary>
        public FieldOperationStatement(string field, FieldOperationStatementType type, ValueStatement value, FieldType fieldType)
        {
            if (string.IsNullOrEmpty(field))
                throw new ArgumentException("field is null or empty.", nameof(field));
            if (value == null)
                throw new ArgumentNullException(nameof(value), "value is null.");
            Field = field;
            Type = type;
            Value = value;
            FieldType = fieldType;
        }


        protected internal override string ConvertToQueryValue()
        {
            var field = $"[{Field}]";
            switch (FieldType)
            {
                case FieldType.Default:                    
                    break;
                case FieldType.Source:
                    field = "Source." + field;
                    break;
                case FieldType.Target:
                    field = "Target." + field;
                    break;           
                default:
                    throw new NotImplementedException();
            }

            string op;

            switch (Type)
            {
                case FieldOperationStatementType.Equals:
                    op = "=";
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

            return $"{field} {op} {Value.ConvertToQueryValue()}";
           
        }
    }
}
