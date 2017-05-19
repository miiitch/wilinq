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
        private readonly Dictionary<string, object> _fields;


        protected T? GetStructField<T>(string referenceName) where T : struct
        {
            if (_fields.TryGetValue(referenceName, out object fieldValue))
            {
                return (T?)fieldValue;
            }
            if (_workItem.Fields != null && _workItem.Fields.TryGetValue(referenceName, out fieldValue))
            {
                return (T?)fieldValue;
            }
            return null;
        }

        protected T GetRefField<T>(string referenceName) where T : class
        {
            if (_fields.TryGetValue(referenceName, out object fieldValue))
            {
                return (T)fieldValue;
            }
            if (_workItem?.Fields != null && _workItem.Fields.TryGetValue(referenceName, out fieldValue))
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
            _fields[referenceName] = nextValue;
        }

        private WorkItem _workItem;

        protected WorkItemBase()
        {
            _fields = new Dictionary<string, object>();
        }

        internal WorkItem WorkItem
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

        [Field(SystemField.Project)]
        public string Project
        {
            get => GetRefField<string>(SystemField.Project);
            set => SetRefField<string>(SystemField.Project, value);
        }


        [Field(SystemField.WorkItemType)]
        public string WorkItemType
        {
            get => GetRefField<string>(SystemField.WorkItemType);
            internal set => SetRefField<string>(SystemField.WorkItemType, value);
        }


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