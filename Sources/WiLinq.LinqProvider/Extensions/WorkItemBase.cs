using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

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
        public enum FieldOperation
        {
            Add, Replace, Remove
        };

        private readonly Dictionary<string,(FieldOperation op, object value)> _changeDictionary = new Dictionary<string, (FieldOperation op, object value)>();


        private void RegisterChange(string referenceName, FieldOperation fieldOperation, object value)
        {
            if (_changeDictionary.TryGetValue(referenceName, out var previousChange))
            {
                switch (fieldOperation)
                {
                    case FieldOperation.Add when previousChange.op == FieldOperation.Remove:
                    case FieldOperation.Replace:
                        _changeDictionary[referenceName] = (FieldOperation.Replace, value);
                        break;
                    case FieldOperation.Remove when previousChange.op == FieldOperation.Add:
                        _changeDictionary.Remove(referenceName);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(fieldOperation), fieldOperation, null);
                }
            }
            else
            {
                _changeDictionary.Add(referenceName, (fieldOperation, value));
            }
        }

        protected T? GetStructField<T>(string referenceName) where T : struct
        {
            if (_workItem.Fields.TryGetValue(referenceName, out object value))
            {
                return (T?)value;
            }
            return null;
        }

        protected T GetRefField<T>(string referenceName) where T : class
        {
            if (_workItem.Fields.TryGetValue(referenceName, out object value))
            {
                return (T)value;
            }
            return null;
        }

        protected void SetStructField<T>(string referenceName, T? nullableValue) where T : struct
        {
            if (referenceName == "System.Id")
            {
                throw new ArgumentException("Can't set Id field");
            }

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
            if (_workItem.Fields.TryGetValue(referenceName, out object currentValue))
            {
                if (nextValue != null) 
                {
                    if (currentValue.Equals(nextValue))
                    {
                        return;
                    }
                    _workItem.Fields[referenceName] = nextValue;
                   RegisterChange(referenceName, FieldOperation.Replace,  nextValue);
                }
                else
                {
                    _workItem.Fields.Remove(referenceName);
                    RegisterChange(referenceName, FieldOperation.Remove, null);
                }
            }
            else
            {
                if (nextValue == null)
                {
                    return;
                }
                _workItem.Fields[referenceName] = nextValue;
                RegisterChange(referenceName, FieldOperation.Add, nextValue);
            }
        }

        private WorkItem _workItem;

        protected WorkItemBase()
        {

        }


        public List<(string ReferenceName,FieldOperation Operation,object Value)> GetFieldOperations()
        {
            return _changeDictionary
                .Select(_ => (_.Key, _.Value.op, _.Value.value))
                .ToList();
        }

        public WorkItem WorkItem
        {
            get
            {
                if (_workItem == null)
                {
                    throw new InvalidOperationException("Workitem not assigned");
                }
                return _workItem;
            }
            set
            {
                if (_workItem != null)
                {
                    throw new InvalidOperationException("Workitem already assigned");
                }
                _workItem = value;
            }
        }

        [Field(SystemField.Id)]
        public int? Id => _workItem.Id;

        [Field(SystemField.AssignedTo)]
        public string AssignedTo
        {
            get => GetRefField<string>(SystemField.AssignedTo);
            set => SetRefField<string>(SystemField.AssignedTo, value);
        }

        [Field(SystemField.Title)]
        public string Title
        {
            get => GetRefField<string>(SystemField.Title);
            set => SetRefField<string>(SystemField.Title, value);
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




        [Field(SystemField.Description)]
        public string Description
        {
            get => GetRefField<string>(SystemField.Description);
            set => SetRefField<string>(SystemField.Description, value);
        }



        [Field(SystemField.Reason)]
        public string Reason
        {
            get => GetRefField<string>(SystemField.Reason);
            set => SetRefField<string>(SystemField.Reason, value);
        }

        //[Field(SystemField.WorkItemType)]
        //public string Type => WorkItem.Type.Name;

        [Field(SystemField.State)]
        public string State
        {
            get => GetRefField<string>(SystemField.State);
            set => SetRefField<string>(SystemField.State, value);
        }

        [Field(SystemField.Revision)]
        public int? Revision => WorkItem.Rev;
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