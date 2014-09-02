using System;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace WiLinq.LinqProvider
{
    // ReSharper disable once InconsistentNaming
    public static class TFSWorkItemExtensions
    {

        public static T Field<T>(this WorkItem wi, string referenceName)
        {
            return (T)(wi.Fields[referenceName].Value);
        }


        public static bool SetField<T>(this WorkItem wi, string referenceName, T value)
        {
            var field = wi.Fields[referenceName];
            var valueType = typeof(T);
            if (field.FieldDefinition.SystemType == valueType)
            {
                field.Value = value;
            }
            else
            {
                var nullableGenericType = typeof (System.Nullable<>);

                var nullableVersionOfFieldType = nullableGenericType.MakeGenericType(field.FieldDefinition.SystemType);

                if (valueType == nullableVersionOfFieldType)
                {
                    var objectValue = (object)value;

                    if (objectValue == null)
                    {
                        field.Value = value;
                    }
                    else
                    {                                                
                        var propertyInfo = nullableVersionOfFieldType.GetProperty("Value");

                        var realValue = propertyInfo.GetValue(objectValue);

                        field.Value = realValue;
                    }


                }
                else
                {
                    throw new ArgumentException(String.Format("Invalid type '{0}' for reference field '{1}'", typeof(T), referenceName));

                }

            }
            return field.IsValid;
        }
    }
}
