﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace WiLinq.LinqProvider
{
    [IgnoreField(SystemField.AreaId)]
    [IgnoreField(SystemField.IterationId)]
    [IgnoreField(SystemField.RelatedLinkCount)]
    [IgnoreField(SystemField.AttachedFileCount)]
    [IgnoreField(SystemField.ExternalLinkCount)]
    [IgnoreField(SystemField.HyperLinkCount)]
    [IgnoreField(SystemField.AttachedFileCount)]
    [IgnoreField(SystemField.NodeName)]
    [IgnoreField(SystemField.Watermark)]
    [IgnoreField(SystemField.History)]
    public class GenericWorkItem
    {
        private static CultureInfo _jsonCulture = new CultureInfo("en-us");

        private Dictionary<string, object> _initialFieldValues;

        private Dictionary<string, object> _fieldValues;


        protected T? GetStructField<T>(string referenceName) where T : struct
        {
            if (_fieldValues.TryGetValue(referenceName, out var fieldValue))
            {
                return (T) fieldValue;
            }
            if (_initialFieldValues != null && _initialFieldValues.TryGetValue(referenceName, out fieldValue))
            {
                var value = (T) fieldValue;
                return value;
            }
            return null;
        }

        protected T GetRefField<T>(string referenceName) where T : class
        {
            if (_fieldValues.TryGetValue(referenceName, out var fieldValue))
            {
                return (T) fieldValue;
            }
            if (_initialFieldValues != null && _initialFieldValues.TryGetValue(referenceName, out fieldValue))
            {
                return (T) fieldValue;
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
            SetFieldValue(referenceName, nullableValue.HasValue ? (object) nullableValue.Value : null);
        }

        protected void SetRefField<T>(string referenceName, T value) where T : class
        {
            if (referenceName == SystemField.Id)
            {
                throw new ArgumentException("Can't set Id field");
            }
            SetFieldValue(referenceName, value);
        }

        private void SetFieldValue(string referenceName, object nextValue)
        {
            _fieldValues[referenceName] = nextValue;
        }

        public GenericWorkItem()
        {
            _fieldValues = new Dictionary<string, object>();
        }

        internal void CopyValuesFromWorkItem(WorkItem workitem)
        {
            _initialFieldValues = workitem.Fields.ToDictionary(_ => _.Key, _ => _.Value);
            _fieldValues = new Dictionary<string, object>();
            Revision = workitem.Rev;
            Id = workitem.Id;
        }

        private static object EncodeValueForJSonDocument(object value)
        {
            switch (value)
            {
                case long l:
                    return l;
                case int i:
                    return i;
                case double d:
                    return d;
                case string s:
                    return s;
                case bool b:
                    return b ? "true" : "false";
                case DateTime dt:
                    return dt.ToString("O", _jsonCulture);
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
                object initialValue = null;

                _initialFieldValues?.TryGetValue(key, out initialValue);
                var hasNewValue = _fieldValues.TryGetValue(key, out var value);

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
        ///     Gets the created by value of the work item.
        /// </summary>
        /// <value>The created by.</value>
        [Field(SystemField.CreatedBy)]
        public IdentityRef CreatedBy => GetRefField<IdentityRef>(SystemField.CreatedBy);

        /// <summary>
        ///     Gets the changed by value of the work item.
        /// </summary>
        /// <value>The changed by.</value>
        [Field(SystemField.ChangedBy)]
        public IdentityRef ChangedBy => GetRefField<IdentityRef>(SystemField.ChangedBy);

        /// <summary>
        ///     Gets the created date.
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

        [Field(SystemField.State)]
        public string State
        {
            get => GetRefField<string>(SystemField.State);
            set => SetRefField(SystemField.State, value);
        }

        [Field(SystemField.Tags)]
        public virtual string Tags
        {
            get => GetRefField<string>("System.Tags");
            set => SetRefField("System.Tags", value);
        }


        [Field("System.BoardColumn")]
        public virtual string BoardColumn => GetRefField<string>("System.BoardColumn");

        [Field("System.BoardColumnDone")]
        public virtual bool? BoardColumnDone => GetStructField<bool>("System.BoardColumnDone");

        [Field("System.BoardLane")]
        public virtual string BoardLane => GetRefField<string>("System.BoardLane");


        [Field(SystemField.AreaPath)]
        public string Area
        {
            get => GetRefField<string>(SystemField.AreaPath);
            set => SetRefField(SystemField.AreaPath, value);
        }

        [Field(SystemField.IterationPath)]
        public string Iteration
        {
            get => GetRefField<string>(SystemField.IterationPath);
            set => SetRefField(SystemField.IterationPath, value);
        }

        private bool IsPathUnder(string referencePath, string path)
        {
            var referencePathElements = SplitPath(referencePath);
            var pathElementsToCompare = SplitPath(path);

            if (pathElementsToCompare.Length < referencePathElements.Length)
            {
                return false;
            }

            for (var i = 0; i < referencePathElements.Length; ++i)
            {
                if (string.Compare(referencePathElements[i], pathElementsToCompare[i],
                        StringComparison.InvariantCulture) != 0)
                {
                    return false;
                }
            }

            return true;


            string[] SplitPath(string pathToSplit)
            {
                if (string.IsNullOrWhiteSpace(pathToSplit))
                {
                    throw new ArgumentException("Value cannot be null or whitespace.", nameof(pathToSplit));
                }


                var pathElements = pathToSplit.Split('\\');

                if (pathElements.Length > 1)
                {
                    var lastElement = pathElements[pathElements.Length - 1];
                    if (string.IsNullOrWhiteSpace(lastElement))
                    {
                        pathElements = pathElements.Take(pathElements.Length - 1).ToArray();
                    }
                }

                if (pathElements.Any(string.IsNullOrWhiteSpace))
                {
                    throw new ArgumentException("invalid path: contains empty elements");
                }

                return pathElements;
            }
        }

        public bool IsUnderArea(string path)
        {
            return IsPathUnder(path, Area);
        }

        public bool IsUnderIteration(string path)
        {
            return IsPathUnder(path, Iteration);
        }
    }
}