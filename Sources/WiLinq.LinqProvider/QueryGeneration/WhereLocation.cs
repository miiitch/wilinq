namespace WiLinq.LinqProvider.QueryGeneration
{
    /// <summary>
    ///     Used to verify query constraints in the where part
    /// </summary>
    internal enum WhereLocation
    {
        LeftOperatorClause,
        RightOperatorClause,
        BooleanOperation
    }
}