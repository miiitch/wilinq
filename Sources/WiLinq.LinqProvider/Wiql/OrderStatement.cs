namespace WiLinq.LinqProvider.Wiql
{
    public sealed class OrderStatement: Statement
    {
        public string Field { get; set; }
        public bool IsAscending { get; set; }

        protected internal override string ConvertToQueryValue() => $"[{Field}] {(IsAscending ? "asc" : "desc")}";
    }
}
