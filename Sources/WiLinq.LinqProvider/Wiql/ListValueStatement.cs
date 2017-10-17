using System;
using System.Collections.Generic;
using System.Linq;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class ListValueStatement : ValueStatement
    {
        /// <summary>
        ///     Initializes a new instance of the ListValueStatement class.
        /// </summary>
        public ListValueStatement(List<ValueStatement> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "value is null.");
            }
            Value = value;
        }

        public List<ValueStatement> Value { get; }

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