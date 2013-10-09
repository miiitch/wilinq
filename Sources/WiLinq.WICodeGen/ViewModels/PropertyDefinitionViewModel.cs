using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.IO;
using GalaSoft.MvvmLight;
using WiLinq.CodeGen.CodeGeneration;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace WiLinq.CodeGen.ViewModels
{
    internal class PropertyDefinitionViewModel : ViewModelBase
    {
        ModelFieldDefinition _field;
        /// <summary>
        /// Initializes a new instance of the PropertyDefinitionViewModel class.
        /// </summary>
        /// <param name="field"></param>
        public PropertyDefinitionViewModel(ModelFieldDefinition field)
        {
            _field = field;
        }

        public string PropertyName
        {
            get
            {
                return _field.PropertyName;
            }
            set
            {
                _field.PropertyName = value;
            }
        }
        public string ReferenceName
        {
            get
            {
                return _field.ReferenceName;
            }
        }

        public string Type
        {
            get
            {
                return _field.TypeNameString;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _field.IsReadOnly;
            }
        }
    }
}
