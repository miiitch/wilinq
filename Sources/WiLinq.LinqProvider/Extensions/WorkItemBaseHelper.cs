using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Linq.Expressions;
using System.Reflection;

namespace WiLinq.LinqProvider.Extensions
{
    class WorkItemBaseHelper<T> : ICustomWorkItemHelper<T> where T : WorkItemBase, new()
    {

        #region ICustomWorkItemHelper<T> Members

        public T CreateItem(WorkItem workitem)
        {
            var ret = new T()
            {
                WorkItem = workitem
            };
            return ret;
        }

        #endregion

        #region ILinqResolver Members

        public WorkItemFieldInfo Resolve(System.Reflection.MemberInfo memberInfo)
        {
            if (memberInfo is PropertyInfo)
            {
                var pInfo = (PropertyInfo)memberInfo;

                var attributes = pInfo.GetCustomAttributes(typeof(FieldAttribute), true) as FieldAttribute[];

                if (attributes.Length != 1)
                {
                    throw new InvalidOperationException("Metadata missing on property " + pInfo.Name);
                }
                return new WorkItemFieldInfo()
                {
                     Name = attributes[0].ReferenceName,
                     Type = pInfo.PropertyType

                };

            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public WorkItemFieldInfo Resolve(string workItemParameterName, MethodCallExpression methodCall)
        {
            throw new NotImplementedException();
        }

        public Tuple<string, string, string> Resolve(MethodCallExpression methodCall, bool isInNotBlock)
        {
            if (methodCall.Method.Name == "IsUnderIteration")
            {
                string refName = SystemField.IterationPath;
                string op = isInNotBlock ? "not under" : "under";
                var valEx = methodCall.Arguments[0] as ConstantExpression;
                var val = valEx.Value as string;
                return new Tuple<string, string, string>(refName, op, val);

            }
            else if (methodCall.Method.Name == "IsUnderArea")
            {
                string refName = SystemField.AreaPath;
                string op = isInNotBlock ? "not under" : "under";
                var valEx = methodCall.Arguments[0] as ConstantExpression;
                var val = valEx.Value as string;
                return new Tuple<string, string, string>(refName, op, val);
            }
            throw new NotImplementedException();
        }

        #endregion
    }
}
