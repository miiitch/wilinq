using System;
using System.Collections.Generic;
using System.Linq;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class StringValueStatement : ValueStatement
    {
        public string Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the StringValueStatement class.
        /// </summary>
        internal StringValueStatement(string value):base()
        {
            if (value == null)
                throw new ArgumentNullException("value is null.", "value");
            Value = value;
        }
        internal StringValueStatement():this(string.Empty)
        {
            
        }
         

        private string Quote(string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("value is null or empty.", "value");

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
