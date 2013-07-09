using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
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
