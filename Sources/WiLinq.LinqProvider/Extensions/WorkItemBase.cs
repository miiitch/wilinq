using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace WiLinq.LinqProvider.Extensions
{


    [IgnoreField(SystemField.AreaId)]
    [IgnoreField(SystemField.IterationId)]
    [IgnoreField(SystemField.RelatedLinkCount)]
    [IgnoreField(SystemField.AttachedFileCount)]
    [IgnoreField(SystemField.ExternalLinkCount)]
    [IgnoreField(SystemField.HyperLinkCount)]
    [IgnoreField(SystemField.AttachedFileCount)]
    [IgnoreField(SystemField.NodeName)]
    public abstract class WorkItemBase
    {
        private Dictionary<string, object> _initialFieldValues;
  
        private Dictionary<string, object> _fieldValues;


        protected T? GetStructField<T>(string referenceName) where T : struct
        {
            if (_fieldValues.TryGetValue(referenceName, out object fieldValue))
            {
                return (T?)fieldValue;
            }
            if (_initialFieldValues != null && _initialFieldValues.TryGetValue(referenceName, out fieldValue))
            {
                return (T?)fieldValue;
            }
            return null;
        }

        protected T GetRefField<T>(string referenceName) where T : class
        {
            if (_fieldValues.TryGetValue(referenceName, out object fieldValue))
            {
                return (T)fieldValue;
            }
            if (_initialFieldValues != null && _initialFieldValues.TryGetValue(referenceName, out fieldValue))
            {
                return (T)fieldValue;
            }
            return null;
        }

        protected void SetStructField<T>(string referenceName, T? nullableValue) where T : struct
        {
            if (referenceName == "System.Id")
            {
                throw new ArgumentException("Can't set Id field");
            }

            // ReSharper disable once MergeConditionalExpression
            SetFieldValue(referenceName, nullableValue.HasValue ? (object)nullableValue.Value : null);
        }

        protected void SetRefField<T>(string referenceName, T value) where T : class
        {
            if (referenceName == "System.Id")
            {
                throw new ArgumentException("Can't set Id field");
            }
            SetFieldValue(referenceName, value);
        }

        private void SetFieldValue(string referenceName, object nextValue)
        {
            _fieldValues[referenceName] = nextValue;
        }
     
        protected WorkItemBase()
        {
            _fieldValues = new Dictionary<string, object>();
        }

        internal void CopyValuesFromWorkItem( WorkItem workitem)
        {
            _initialFieldValues = workitem.Fields.ToDictionary(_ => _.Key, _ => _.Value);
            _fieldValues = new Dictionary<string, object>();
            Revision = workitem.Rev;
            Id = workitem.Id;
        }
        private static string EncodeValueForJSonDocument(object value)
        {
            switch (value)
            {
                case int i:
                    return i.ToString("D");
                case double d:
                    return d.ToString("F");
                case string s:
                    return s;
                case bool b:
                    return b ? "true" : "false";
                case DateTime dt:
                    return $"{dt.ToShortDateString()} {dt.ToShortTimeString()}";
                default:
                    throw new InvalidOperationException($"Type {value.GetType().FullName} not supported as value");
            }
        }
        internal JsonPatchDocument CreatePatchDocument()
        {
            var result = new JsonPatchDocument();

            var keys = _fieldValues.Keys.ToList();

            if (_initialFieldValues != null)
            {
                keys = keys.Union(_initialFieldValues.Keys).Distinct().OrderBy(_ => _).ToList();
            }

            keys.Remove(SystemField.WorkItemType);
            keys.Remove(SystemField.Project);

            foreach (var key in keys)
            {
                object value;
                object initialValue = null;

                _initialFieldValues?.TryGetValue(key, out initialValue);
                var hasNewValue = _fieldValues.TryGetValue(key, out value);

                if (initialValue == null && value != null)
                {
                    var operation = new JsonPatchOperation
                    {
                        Operation = Operation.Add,
                        Path = $"/fields/{key}",
                        Value = EncodeValueForJSonDocument(value)
                    };
                    result.Add(operation);
                }
                else if (hasNewValue)
                {
                    if (value == null)
                    {
                        var operation = new JsonPatchOperation
                        {
                            Operation = Operation.Remove,
                            Path = $"/fields/{key}"
                        };
                        result.Add(operation);
                    }
                    else
                    {
                        var operation = new JsonPatchOperation
                        {
                            Operation = Operation.Replace,
                            Path = $"/fields/{key}",
                            Value = EncodeValueForJSonDocument(value)
                        };
                        result.Add(operation);
                    }
                }                     
            }
            return result;
        }


        [Field(SystemField.Id)]
        public int? Id { get; private set; }
    

        [Field(SystemField.Revision)]
        public int? Revision { get; private set; }

        [Field(SystemField.AssignedTo)]
        public string AssignedTo
        {
            get => GetRefField<string>(SystemField.AssignedTo);
            set => SetRefField(SystemField.AssignedTo, value);
        }

        [Field(SystemField.Title)]
        public string Title
        {
            get => GetRefField<string>(SystemField.Title);
            set => SetRefField(SystemField.Title, value);
        }

        [Field(SystemField.ChangedDate)]
        public DateTime? ChangedDate => GetStructField<DateTime>(SystemField.ChangedDate);

        /// <summary>
        /// Gets the created by value of the work item.
        /// </summary>
        /// <value>The created by.</value>
        [Field(SystemField.CreatedBy)]
        public string CreatedBy => GetRefField<string>(SystemField.CreatedBy);

        /// <summary>
        /// Gets the changed by value of the work item.
        /// </summary>
        /// <value>The changed by.</value>
        [Field(SystemField.ChangedBy)]
        public string ChangedBy => GetRefField<string>(SystemField.ChangedBy);

        /// <summary>
        /// Gets the created date.
        /// </summary>
        /// <value>The created date.</value>
        [Field(SystemField.CreatedDate)]
        public DateTime? CreatedDate => GetStructField<DateTime>(SystemField.CreatedDate);

        [Field(SystemField.Project)]
        public string Project
        {
            get => GetRefField<string>(SystemField.Project);
            set => SetRefField(SystemField.Project, value);
        }


        [Field(SystemField.WorkItemType)]
        public string WorkItemType
        {
            get => GetRefField<string>(SystemField.WorkItemType);
            internal set => SetRefField(SystemField.WorkItemType, value);
        }


        [Field(SystemField.Description)]
        public string Description
        {
            get => GetRefField<string>(SystemField.Description);
            set => SetRefField(SystemField.Description, value);
        }



        [Field(SystemField.Reason)]
        public string Reason
        {
            get => GetRefField<string>(SystemField.Reason);
            set => SetRefField(SystemField.Reason, value);
        }

      

        //[Field(SystemField.WorkItemType)]
        //public string Type => WorkItem.Type.Name;

        [Field(SystemField.State)]
        public string State
        {
            get => GetRefField<string>(SystemField.State);
            set => SetRefField(SystemField.State, value);
        }

      




#if false
        [Field(SystemField.AreaPath)]
         public string Area => WorkItem.AreaPath;

        [Field(SystemField.IterationPath)]
         public string Iteration => WorkItem.IterationPath;

        public bool IsUnderArea(string areaPath)
         {
             if (string.IsNullOrEmpty(areaPath))
                 throw new ArgumentException("areaPath is null or empty.", nameof(areaPath));

             return areaPath.Contains(Area);
         }

         public bool IsUnderIteration(string iterationPath)
         {
             if (string.IsNullOrEmpty(iterationPath))
                 throw new ArgumentException("iterationPath is null or empty.", nameof(iterationPath));

             return iterationPath.Contains(Iteration);
         }
#endif
    }
}