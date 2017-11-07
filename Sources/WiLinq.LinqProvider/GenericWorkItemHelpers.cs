using System;
using System.Collections.Generic;
using System.Reflection;
using WiLinq.LinqProvider.Extensions;

namespace WiLinq.LinqProvider
{
    internal static class GenericWorkItemHelpersCore
    {
        public static string GetWorkItemType(Type workItemType)
        {

            if (!(workItemType.GetCustomAttributes(typeof(WorkItemTypeAttribute), false) is WorkItemTypeAttribute[]
                    modelAttribs) || modelAttribs.Length != 1)
            {
                return null;
            }
            else
            {
                return  modelAttribs[0].TypeName;
            }
        }

        public static string GetWorkItemType<T>() where T: GenericWorkItem
        {
            return GetWorkItemType(typeof(T));
        }
    }

    /// <summary>
    ///     Utility class used to discover and manipulate specialized workitem
    /// </summary>
    /// <typeparam name="T">Subclass of WorkItemObject</typeparam>
    public static class GenericWorkItemHelpers<T> where T : GenericWorkItem, new()
    {
        internal class FieldData
        {
            public string ReferenceName { get; set; }
            public Type Type { get; set; }
        }


        internal static List<FieldData> Fields { get; private set; }

        // ReSharper disable StaticFieldInGenericType
        private static string _workItemTypeName;

        private static bool _typeProcessed;

        private static bool _propertyProcessed;
        // ReSharper restore StaticFieldInGenericType        

        internal static ICustomWorkItemHelper<T> Provider => new WorkItemBaseHelper<T>();


        /// <summary>
        ///     Processes the work item type and extracts the useful data from its properties
        /// </summary>
        private static void ProcessProperties()
        {
            if (_propertyProcessed)
            {
                return;
            }
            _propertyProcessed = true;
            Fields = new List<FieldData>();


            var props = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public |
                                                BindingFlags.DeclaredOnly);

            foreach (var prop in props)
            {
                var fieldAttribs = prop.GetCustomAttributes(typeof(FieldAttribute), false) as FieldAttribute[];

                if (fieldAttribs == null || fieldAttribs.Length != 1)
                {
                    continue;
                }

                Fields.Add(new FieldData {ReferenceName = fieldAttribs[0].ReferenceName, Type = prop.PropertyType});
            }
        }

        /// <summary>
        ///     Processes the work item type and extracts the useful data
        /// </summary>
        private static void ProcessType()
        {
            if (_typeProcessed)
            {
                return;
            }
            _typeProcessed = true;

            _workItemTypeName = GenericWorkItemHelpersCore.GetWorkItemType<T>();
          
        }


        /// <summary>
        ///     Gets the name of the work item type.
        /// </summary>
        /// <value>The name of the work item type.</value>
        public static string WorkItemTypeName
        {
            get
            {
                ProcessType();
                return _workItemTypeName;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the work item is valid for Linq to WIQL queries
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public static bool IsValid
        {
            get
            {
                ProcessType();

                return typeof(T) == typeof(GenericWorkItem) || !string.IsNullOrEmpty(_workItemTypeName);
            }
        }
#if false
        public static bool MatchWorkItemType(WorkItemType type)
        {
            if (type == null)
            {
                return false;
            }

            if (type.Name != WorkItemTypeName)
            {
                return false;
            }

            ProcessProperties();

            foreach (var propData in _fieldDataList)
            {
                var matchingField = (from field in type.FieldDefinitions.Cast<FieldDefinition>()
                                     where field.ReferenceName == propData.ReferenceName
                                     select field).FirstOrDefault();

                if (matchingField == null)
                {
                    return false;
                }

                if (propData.Type == matchingField.SystemType)
                {
                    continue;
                }

                if (matchingField.SystemType.IsClass)
                {
                    return false;
                }

                var nullableType = typeof(Nullable<>).MakeGenericType(new[] { matchingField.SystemType });
                if (propData.Type == nullableType)
                {
                    continue;
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifies if the project can be used for the current work item type
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns></returns>
        public static bool CheckProjectUsability(ProjectInfo project)
        {

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (WorkItemType wiType in project.WorkItemTypes)
            {
                if (wiType.Name == WorkItemTypeName)
                {
                    return MatchWorkItemType(wiType);
                }
            }
            return false;
        }
#endif
    }
}