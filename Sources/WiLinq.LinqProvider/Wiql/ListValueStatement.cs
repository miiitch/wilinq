using System;
using System.Collections.Generic;
using System.Linq;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class ListValueStatement : ValueStatement
    {
        public List<ValueStatement> Value { get; private set; }
        /// <summary>
        /// Initializes a new instance of the ListValueStatement class.
        /// </summary>
        public ListValueStatement(List<ValueStatement> value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "value is null.");
            Value = value;
        }

        protected internal override string ConvertToQueryValue()
        {
            var q = Value.Select(statement => statement.ConvertToQueryValue()).ToArray();

            var ret = $"({string.Join(",", q)})";

            return ret;
        }

        public override ValueStatement Copy()
        {
            return new ListValueStatement(Value.Select(v => v.Copy()).ToList());
        }
    }
}
