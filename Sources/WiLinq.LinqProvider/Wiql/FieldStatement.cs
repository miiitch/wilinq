namespace WiLinq.LinqProvider.Wiql
{
    public sealed class FieldStatement : Statement
    {
        public FieldStatement(string field, FieldOrigin origin)
        {
            Field = field;
            Origin = origin;
        }

        public string Field { get; }

        public FieldOrigin Origin { get; }

        protected internal override string ConvertToQueryValue()
        {
            switch (Origin)
            {
                case FieldOrigin.Source:
                    return $"Source.[{Field}]";
                  
                case FieldOrigin.Target:
                    return $"Target.[{Field}]";

                default:
                    return $"[{Field}]";
            }
            
        }
    }
}