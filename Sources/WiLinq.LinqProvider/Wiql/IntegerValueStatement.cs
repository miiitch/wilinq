using System;
using System.Collections.Generic;
using System.Linq;

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
            return Value.ToString();
        }

        public override ValueStatement Copy()
        {
            return new IntegerValueStatement(Value);
        }
    }
}