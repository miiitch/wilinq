using GalaSoft.MvvmLight;
using WiLinq.CodeGen.CodeGeneration;

namespace WiLinq.CodeGen.ViewModels
{
    internal class PropertyDefinitionViewModel : ViewModelBase
    {
        private readonly ModelFieldDefinition _field;
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
            get => _field.PropertyName;
            set => _field.PropertyName = value;
        }
        public string ReferenceName => _field.ReferenceName;

        public string Type => _field.TypeNameString;

        public bool IsReadOnly => _field.IsReadOnly;
    }
}
