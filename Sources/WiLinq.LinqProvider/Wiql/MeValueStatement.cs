namespace WiLinq.LinqProvider.Wiql
{
    public sealed class MeValueStatement : ValueStatement
    {
  
        internal MeValueStatement()
        {

        }

        protected internal override string ConvertToQueryValue() => "@Me";

        public override ValueStatement Copy() => this;
    }
}
