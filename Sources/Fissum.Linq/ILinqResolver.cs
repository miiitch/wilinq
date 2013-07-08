using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Linq.Expressions;
using System.Reflection;

namespace WiLinq.LinqProvider
{
    public interface ILinqResolver
    {
        WorkItemFieldInfo Resolve(MemberInfo memberInfo);
        WorkItemFieldInfo Resolve(string workItemParameterName, MethodCallExpression methodCall);

        /// <summary>
        /// Resolve method on methodcall on the workitem type itself
        /// </summary>
        /// <param name="methodCall"></param>
        /// <returns></returns>
        Tuple<string, string, string> Resolve(MethodCallExpression methodCall, bool isInNotBlock);
    }
}
