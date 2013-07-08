using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using WiLinq.LinqProvider.Extensions;



namespace WiLinq.LinqProvider
{
#if false

    internal static class FieldUtility
    {

        public static string ExtractFieldReferenceName(ICustomAttributeProvider methodInfo)
        {
            FieldAttribute[] array = methodInfo.GetCustomAttributes(typeof(FieldAttribute), false) as FieldAttribute[];

            if (array == null)
            {
                return null;
            }

            return String.Format("[{0}]", array[0].ReferenceName);
        }

        public static  string ExtractWIFieldFromMethodCall(MethodCallExpression m)
        {
            switch (m.Method.Name)
            {
                case "Field":
                    {                      
                        if (m.Arguments.Count != 1)
                        {
                            throw new InvalidOperationException("MethodCall");
                        }

                        ConstantExpression constant = m.Arguments[0] as ConstantExpression;

                        if (constant == null)
                        {
                            throw new InvalidOperationException("MethodCall");
                        }

                        return "[" + constant.Value + "]";
                    }                    
                default:
                    return null;
            }
        }
    }
#endif
}
