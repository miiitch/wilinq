using System.Collections.Generic;

namespace WiLinq.CodeGen.CodeGeneration
{
    public class ModelClassDefinition
    {
        public string WorkItemType { get; set; }

        public string ClassName { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public bool Generate { get; set; }

        public List<ModelFieldDefinition> FieldDefinitions { get; set; }        
    }
}
