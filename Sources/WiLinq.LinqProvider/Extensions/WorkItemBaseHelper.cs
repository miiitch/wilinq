using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace WiLinq.LinqProvider.Extensions
{
    internal class WorkItemBaseHelper<T> : ICustomWorkItemHelper<T> where T : WorkItemBase, new()
    {

        #region ICustomWorkItemHelper<T> Members

        public T CreateItem(WorkItem workitem)
        {
            var ret = new T
            {
                WorkItem = workitem
            };
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

        public Tuple<string, string, string> Resolve(MethodCallExpression methodCall, bool isInNotBlock)
        {
            if (methodCall.Method.Name == "IsUnderIteration")
            {
                const string refName = SystemField.IterationPath;
                var op = isInNotBlock ? "not under" : "under";
                var valEx = methodCall.Arguments[0] as ConstantExpression;
                if (valEx == null)
                {
                    throw new InvalidOperationException();
                }
                var val = valEx.Value as string;
                return new Tuple<string, string, string>(refName, op, val);

            }
            if (methodCall.Method.Name == "IsUnderArea")
            {
                const string refName = SystemField.AreaPath;
                var op = isInNotBlock ? "not under" : "under";
                var valEx = methodCall.Arguments[0] as ConstantExpression;                
                if (valEx == null)
                {
                    throw new InvalidOperationException();
                }
                var val = valEx.Value as string;
                return new Tuple<string, string, string>(refName, op, val);
            }
            throw new NotImplementedException();
        }

        #endregion
    }
}
