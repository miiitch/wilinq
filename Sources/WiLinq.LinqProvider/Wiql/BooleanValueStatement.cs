using System;

namespace WiLinq.LinqProvider.Wiql
{
    public class BooleanValueStatement : ValueStatement
    {
        private readonly bool _value;

        public BooleanValueStatement(bool value)
        {
            _value = value;
        }


        protected internal override string ConvertToQueryValue()
        {
            throw new NotImplementedException();
        }

        public override ValueStatement Clone()
        {
            return new BooleanValueStatement(_value);
        }
    }
}