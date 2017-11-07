using System;
using System.Linq.Expressions;
using System.Reflection;
using WiLinq.LinqProvider.Wiql;

namespace WiLinq.LinqProvider.Extensions
{
    internal class CustomWorkItemResolver<T> : ILinqResolver where T : GenericWorkItem
    {
        #region ILinqResolver Members

        public WorkItemFieldInfo Resolve(MemberInfo memberInfo)
        {
            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new NotImplementedException();
            }
            var pInfo = propertyInfo;

            if (!(pInfo.GetCustomAttributes(typeof(FieldAttribute), true) is FieldAttribute[] attributes) 
                || attributes.Length != 1)
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

        public (WorkItemFieldInfo field, FieldOperationStatementType op, object value) Resolve(MethodCallExpression methodCall,
            bool isInNotBlock)
        {
            switch (methodCall.Method.Name)
            {
                case nameof(GenericWorkItem.IsUnderIteration):
                {
                    const string refName = SystemField.IterationPath;
                    var op = isInNotBlock ? FieldOperationStatementType.IsNotUnder : FieldOperationStatementType.IsUnder;
                    if (!(methodCall.Arguments[0] is ConstantExpression valEx))
                    {
                        throw new InvalidOperationException();
                    }
                    var val = valEx.Value as string;
                    return (new WorkItemFieldInfo {Name = refName, Type = typeof(string)}, op, val);
                }
                case nameof(GenericWorkItem.IsUnderArea):
                {
                    const string refName = SystemField.AreaPath;
                    var op = isInNotBlock ? FieldOperationStatementType.IsNotUnder : FieldOperationStatementType.IsUnder;
                        if (!(methodCall.Arguments[0] is ConstantExpression valEx))
                    {
                        throw new InvalidOperationException();
                    }
                    var val = valEx.Value as string;
                    return (new WorkItemFieldInfo {Name = refName, Type = typeof(string)}, op, val);
                }

                default:
                    throw new NotImplementedException();
            }
        }

        #endregion
    }
}