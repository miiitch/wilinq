namespace WiLinq.LinqProvider.Wiql
{
    public sealed class ProjectValueStatement : ValueStatement
    {
        internal ProjectValueStatement()
        {
        }

        protected internal override string ConvertToQueryValue()
        {
            return "@project";
        }

        public override ValueStatement Clone()
        {
            return this;
        }
    }
}