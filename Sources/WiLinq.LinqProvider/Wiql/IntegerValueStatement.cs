using System.Globalization;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class IntegerValueStatement : ValueStatement
    {
        public int Value { get; private set; }

        internal IntegerValueStatement(int value)
        {
            Value = value;
        }

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