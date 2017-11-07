using System.Linq.Expressions;
using System.Reflection;
using WiLinq.LinqProvider.Wiql;

namespace WiLinq.LinqProvider
{
    internal interface ILinqResolver
    {
        WorkItemFieldInfo Resolve(MemberInfo memberInfo);
        WorkItemFieldInfo Resolve(string workItemParameterName, MethodCallExpression methodCall);

        /// <summary>
        ///     Resolve method on methodcall on the workitem type itself
        /// </summary>
        /// <param name="methodCall"></param>
        /// <param name="isInNotBlock"></param>
        /// <returns></returns>
        (WorkItemFieldInfo field, FieldOperationStatementType op, object value) Resolve(MethodCallExpression methodCall, bool isInNotBlock);
    }
}