using System;
using System.Collections.Generic;
using System.Linq;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class ProjectValueStatement : ValueStatement
    {
       
   
        internal ProjectValueStatement():base()
        {

        }

        protected internal override string ConvertToQueryValue()
        {
            return "@project";
        }

        public override ValueStatement Copy()
        {
            return this;
        }
    }
}
