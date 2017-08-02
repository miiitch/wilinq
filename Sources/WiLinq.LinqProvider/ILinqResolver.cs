using System;
using System.Linq.Expressions;
using System.Reflection;

namespace WiLinq.LinqProvider
{
    internal interface ILinqResolver
    {
        WorkItemFieldInfo Resolve(MemberInfo memberInfo);
        WorkItemFieldInfo Resolve(string workItemParameterName, MethodCallExpression methodCall);

        /// <summary>
        /// Resolve method on methodcall on the workitem type itself
        /// </summary>
        /// <param name="methodCall"></param>
        /// <param name="isInNotBlock"></param>
        /// <returns></returns>
        (string field,string op, string value) Resolve(MethodCallExpression methodCall, bool isInNotBlock);
    }
}
