using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WiLinq.LinqProvider
{
    internal interface IWorkItemLinqQueryProvider : IQueryProvider
    {
        string GetQueryText(Expression expression);
        DateTime? AsOfDate { get; set; }
        TPCQuery TransformAsWorkItemQuery(Expression expression);

        Task<object> ExecuteAync(Expression expression);
        Task<List<int>> ExecuteAndGetIdsAsync(Expression expression);

    }
}
