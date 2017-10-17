namespace WiLinq.LinqProvider.Wiql
{
    public sealed class MeValueStatement : ValueStatement
    {
        internal MeValueStatement()
        {
        }

        protected internal override string ConvertToQueryValue()
        {
            return "@Me";
        }

        public override ValueStatement Copy()
        {
            return this;
        }
    }
}