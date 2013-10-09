using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using System.IO;

namespace WiLinq.CodeGen.CodeGeneration
{
    /// <summary>
    /// Represents a work item model definition data
    /// </summary>
    public class ModelDefinition
    {
     
        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the class definitions.
        /// </summary>
        /// <value>The class definitions.</value>
        public List<ModelClassDefinition> ClassDefinitions { get; set; }


    
    }
}