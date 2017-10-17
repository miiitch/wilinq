using System.Globalization;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class IntegerValueStatement : ValueStatement
    {
        internal IntegerValueStatement(int value)
        {
            Value = value;
        }

        public int Value { get; }

        protected internal override string ConvertToQueryValue()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        public override ValueStatement Copy()
        {
            return new IntegerValueStatement(Value);
        }
    }
}