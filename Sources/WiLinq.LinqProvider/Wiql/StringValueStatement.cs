using System;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class StringValueStatement : ValueStatement
    {
        public string Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the StringValueStatement class.
        /// </summary>
        internal StringValueStatement(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "value");
            Value = value;
        }
        internal StringValueStatement():this(string.Empty)
        {
            
        }
         

        private string Quote(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("value is null or empty.", nameof(value));

            return value.Replace("'", "''");
        }
        protected internal override string ConvertToQueryValue()
        {
            return string.Format("'{0}'", Quote(Value));
        }

        public override ValueStatement Copy()
        {
            return new StringValueStatement(Value);
        }

    }
}
