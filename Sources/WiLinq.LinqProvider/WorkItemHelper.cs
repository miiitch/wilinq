using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace WiLinq.LinqProvider
{
    // ReSharper disable once InconsistentNaming
    internal sealed class WorkItemHelper : ICustomWorkItemHelper<WorkItem>
    {
        #region ICustomWorkItemHelper<WorkItem> Members

        public WorkItem CreateItem(WorkItem workitem)
        {
            return workitem;
        }

        public WorkItemFieldInfo Resolve(MemberInfo memberInfo)
        {
            if (memberInfo.MemberType == MemberTypes.Property)
            {
                var prop = memberInfo as PropertyInfo;
                if (prop == null)
                {
                    throw new InvalidOperationException();
                }

                switch (prop.Name)
                {
                    case nameof(WorkItem.Id):
                        return WorkItemFieldInfo.CreateIntField(SystemField.Id);
                    default:
                        return null;
                }
            }
            return null;
        }


        public WorkItemFieldInfo Resolve(string workItemParameterName, MethodCallExpression methodCall)
        {
            if (methodCall.Method.DeclaringType == typeof(WorkItemExtensions) &&
                methodCall.Method.Name == nameof(WorkItemExtensions.Field) &&
                methodCall.Arguments.Count == 2 &&
                methodCall.Method.IsGenericMethod)
            {
                if (!(methodCall.Arguments[0] is ParameterExpression wiParam) 
                    || wiParam.Name != workItemParameterName)
                {
                    throw new InvalidOperationException("Invalid Expression");
                }

                if (!(methodCall.Arguments[1] is ConstantExpression fieldName) 
                    || fieldName.Type != typeof(string))
                {
                    throw new InvalidOperationException("Invalid Expression");
                }

                var returnType = methodCall.Method.GetGenericArguments()[0];

                return new WorkItemFieldInfo
                {
                    Name = fieldName.Value as string,
                    Type = returnType
                };
            }
            return null;
        }

        public (WorkItemFieldInfo field, string op, object value) Resolve(MethodCallExpression methodCall,
            bool isInNotBlock)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}