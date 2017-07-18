using System.Globalization;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class IntegerValueStatement : ValueStatement
    {
        public int Value { get; }

        internal IntegerValueStatement(int value)
        {
            Value = value;
        }

        protected internal override string ConvertToQueryValue() => Value.ToString(CultureInfo.InvariantCulture);

        public override ValueStatement Copy() => new IntegerValueStatement(Value);
    }
}