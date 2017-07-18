using System;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class DateValueStatement : ValueStatement
    {
        public DateTime Value { get; }
        /// <summary>
        /// Initializes a new instance of the DateValue class.
        /// </summary>
        internal DateValueStatement(DateTime date)
        {
            Value = date;
        }

        protected internal override string ConvertToQueryValue() => $"'{Value:O}'";

        public override ValueStatement Copy() => new DateValueStatement(Value);
    }
}
