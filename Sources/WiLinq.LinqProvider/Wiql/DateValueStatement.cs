using System;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class DateValueStatement : ValueStatement
    {
        /// <summary>
        ///     Initializes a new instance of the DateValue class.
        /// </summary>
        internal DateValueStatement(DateTime date)
        {
            Value = date;
        }

        public DateTime Value { get; }

        protected internal override string ConvertToQueryValue()
        {
            return $"'{Value:O}'";
        }

        public override ValueStatement Copy()
        {
            return new DateValueStatement(Value);
        }
    }
}