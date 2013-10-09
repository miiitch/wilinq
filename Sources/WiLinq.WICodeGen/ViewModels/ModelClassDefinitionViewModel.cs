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
    internal class ModelClassDefinitionViewModel : ViewModelBase
    {
        ModelClassDefinition _model;


        public List<PropertyDefinitionViewModel> Properties { get; private set; }


        public bool Generate
        {
            get
            {
                return _model.Generate;
            }
            set
            {
                _model.Generate = value;
                RaisePropertyChanged(() => Generate);
            }
        }

        /// <summary>    
        /// </summary>
        /// <param name="model"></param>
        public ModelClassDefinitionViewModel(ModelClassDefinition model)
        {
            _model = model;

            Properties = _model.FieldDefinitions
                .OrderBy(field => field.Name)
                .Where(field => !field.Inherited)
                .Select(field => new PropertyDefinitionViewModel(field))
                .ToList();

            Generate = true;
        }

        public string ClassName
        {
            get
            {
                return _model.ClassName;
            }
            set
            {
                _model.ClassName = value;
            }
        }

        public string WorkItemType
        {
            get
            {
                return _model.WorkItemType;
            }
        }


        public bool IsPublic
        {
            get
            {
                return _model.IsPublic;
            }
            set
            {
                if (_model.IsPublic != value)
                {
                    _model.IsPublic = value;
                    RaisePropertyChanged(() => IsInternal);
                    RaisePropertyChanged(() => IsPublic);
                }
            }
        }

        public bool IsInternal
        {
            get
            {
                return !_model.IsPublic;
            }
            set
            {
                var isPublic = !value;
                if (_model.IsPublic != isPublic)
                {
                    _model.IsPublic = isPublic;
                    RaisePropertyChanged(() => IsInternal);
                    RaisePropertyChanged(() => IsPublic);
                }
            }
        }

    }
}
