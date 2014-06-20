using System;
using System.Linq;
using System.Linq.Expressions;

namespace WiLinq.LinqProvider
{
    internal interface IWorkItemLinqQueryProvider : IQueryProvider
    {
        string GetQueryText(Expression expression);
        DateTime? AsOfDate { get; set; }
        TPCQuery TransformAsWorkItemQuery(Expression expression);
    }
}
