using System;
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
        /// <param name="isInNotBlock"></param>
        /// <returns></returns>
        Tuple<string, string, string> Resolve(MethodCallExpression methodCall, bool isInNotBlock);
    }
}
