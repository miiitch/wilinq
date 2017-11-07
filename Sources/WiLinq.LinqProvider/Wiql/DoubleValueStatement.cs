using System.Globalization;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class DoubleValueStatement : ValueStatement
    {
        internal DoubleValueStatement(double value)
        {
            Value = value;
        }

        public double Value { get; }

        protected internal override string ConvertToQueryValue()
        {
            return Value.ToString("F", CultureInfo.InvariantCulture);
        }

        public override ValueStatement Clone()
        {
            return new DoubleValueStatement(Value);
        }
    }
}