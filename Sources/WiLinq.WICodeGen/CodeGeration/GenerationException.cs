using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiLinq.CodeGen.CodeGeneration
{
    /// <summary>
    /// Exception thrown when a generation error occured
    /// </summary>
    public class GenerationException: Exception
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="GenerationException"/> class.
        /// </summary>
        public GenerationException(): base()
        {

        }



        /// <summary>
        /// Initializes a new instance of the <see cref="GenerationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public GenerationException(string message)
            : base(message)
        {

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="GenerationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        public GenerationException(string message, params object[] args) :
            base(String.Format(message, args))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public GenerationException(string message, Exception innerException)
            : base(message, innerException)
        {


        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerationException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="args">The args.</param>
        public GenerationException(string message, Exception innerException, params object[] args) :
            base(String.Format(message, args),innerException)
        {

        }

    }
}
