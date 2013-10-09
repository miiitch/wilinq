using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiLinq.CodeGen.CodeGeneration
{
    public class ModelFieldDefinition
    {
        public string Name { get; set; }
        public string ReferenceName { get; set; }
        public Type Type { get; set; }
        public bool IsReadOnly { get; set; }
        public string PropertyName { get; set; }
        public string Description { get; set; }
        public bool Inherited { get; set; }
        public string TypeNameString
        {
            get
            {
                return Type.Name;
            }
        }
    }
}
