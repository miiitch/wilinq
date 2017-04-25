using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace WiLinq.LinqProvider
{
    // ReSharper disable once InconsistentNaming
    internal sealed class TFSWorkItemHelper : ICustomWorkItemHelper<WorkItem>
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
                    case "Title":
                        return WorkItemFieldInfo.CreateStringField(SystemField.Title);
                    case "State":
                        return WorkItemFieldInfo.CreateStringField(SystemField.State);
                    case "WorkItemType":
                        return WorkItemFieldInfo.CreateStringField(SystemField.WorkItemType);
                    case "ChangedBy":
                        return WorkItemFieldInfo.CreateStringField(SystemField.ChangedBy);
                    case "ChangedDate":
                        return WorkItemFieldInfo.CreateDateField(SystemField.ChangedDate);
                    case "CreatedBy":
                        return WorkItemFieldInfo.CreateStringField(SystemField.CreatedBy);
                    case "CreatedDate":
                        return WorkItemFieldInfo.CreateDateField(SystemField.CreatedDate);
                    case "Description":
                        return WorkItemFieldInfo.CreateStringField(SystemField.Description);
                    case "Reason":
                        return WorkItemFieldInfo.CreateStringField(SystemField.Reason);
                    case "Id":
                        return WorkItemFieldInfo.CreateIntField(SystemField.Id);
                    default:
                        return null;
                }
            }
            return null;
        }


        public WorkItemFieldInfo Resolve(string workItemParameterName, MethodCallExpression methodCall)
        {
            if (methodCall.Method.DeclaringType == typeof(TFSWorkItemExtensions) &&
                methodCall.Method.Name == "Field" &&
                methodCall.Arguments.Count == 2 &&
                methodCall.Method.IsGenericMethod)
            {
                var wiParam = methodCall.Arguments[0] as ParameterExpression;
                if (wiParam == null || wiParam.Name != workItemParameterName)
                {
                    throw new InvalidOperationException("Invalid Expression");
                }

                var fieldName = methodCall.Arguments[1] as ConstantExpression;

                if (fieldName == null || fieldName.Type != typeof(string))
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

        public Tuple<string, string, string> Resolve(MethodCallExpression methodCall, bool isInNotBlock)
        {
            throw new NotImplementedException();
        }
        #endregion
    }




}
