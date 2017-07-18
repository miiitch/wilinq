namespace WiLinq.LinqProvider.Wiql
{
    public sealed class ProjectValueStatement : ValueStatement
    {
       
   
        internal ProjectValueStatement()
        {

        }

        protected internal override string ConvertToQueryValue() => "@project";

        public override ValueStatement Copy() => this;
    }
}
