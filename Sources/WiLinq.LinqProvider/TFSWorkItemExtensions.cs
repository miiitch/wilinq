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

            if (field.FieldDefinition.SystemType != typeof(T))
            {
                throw new ArgumentException(String.Format("Invalid type '{0}' for reference field '{1}'", typeof(T), referenceName));
            }
            field.Value = value;
            return field.IsValid;
        }
    }
}
