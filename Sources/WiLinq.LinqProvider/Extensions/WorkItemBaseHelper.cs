using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace WiLinq.LinqProvider.Extensions
{
    internal class WorkItemBaseHelper<T> : ICustomWorkItemHelper<T> where T : GenericWorkItem, new()
    {

        #region ICustomWorkItemHelper<T> Members

        public T CreateItem(WorkItem workitem)
        {
            var ret = new T();
            ret.CopyValuesFromWorkItem(workitem);
            return ret;
        }

        #endregion

        #region ILinqResolver Members

        public WorkItemFieldInfo Resolve(MemberInfo memberInfo)
        {
            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new NotImplementedException();
            }
            var pInfo = propertyInfo;

            var attributes = pInfo.GetCustomAttributes(typeof(FieldAttribute), true) as FieldAttribute[];

            if (attributes == null || attributes.Length != 1)
            {
                throw new InvalidOperationException("Metadata missing on property " + pInfo.Name);
            }
            return new WorkItemFieldInfo
            {
                Name = attributes[0].ReferenceName,
                Type = pInfo.PropertyType

            };
        }

        public WorkItemFieldInfo Resolve(string workItemParameterName, MethodCallExpression methodCall)
        {
            throw new NotImplementedException();
        }

        public (WorkItemFieldInfo field, string op, object value) Resolve(MethodCallExpression methodCall, bool isInNotBlock)
        {
            switch (methodCall.Method.Name)
            {
                case "IsUnderIteration":
                    {
                        const string refName = SystemField.IterationPath;
                        var op = isInNotBlock ? "not under" : "under";
                        if (!(methodCall.Arguments[0] is ConstantExpression valEx))
                        {
                            throw new InvalidOperationException();
                        }
                        var val = valEx.Value as string;
                        return (new WorkItemFieldInfo() { Name = refName, Type = typeof(string) }, op, val);

                    }
                case "IsUnderArea":
                    {
                        const string refName = SystemField.AreaPath;
                        var op = isInNotBlock ? "not under" : "under";
                        if (!(methodCall.Arguments[0] is ConstantExpression valEx))
                        {
                            throw new InvalidOperationException();
                        }
                        var val = valEx.Value as string;
                        return (new WorkItemFieldInfo() { Name = refName, Type = typeof(string) }, op, val);
                    }

                default:
                    throw new NotImplementedException();
            }
            
        }

        #endregion
    }
}
