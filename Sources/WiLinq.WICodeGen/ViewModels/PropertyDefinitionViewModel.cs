using GalaSoft.MvvmLight;
using WiLinq.CodeGen.CodeGeneration;

namespace WiLinq.CodeGen.ViewModels
{
    internal class PropertyDefinitionViewModel : ViewModelBase
    {
        readonly ModelFieldDefinition _field;
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
