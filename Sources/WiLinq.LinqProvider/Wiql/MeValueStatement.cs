using System;
using System.Collections.Generic;
using System.Linq;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class MeValueStatement : ValueStatement
    {
  
        internal MeValueStatement():base()
        {

        }

        protected internal override string ConvertToQueryValue()
        {
            return "@Me";
        }

        public override ValueStatement Copy()
        {
            return this;
        }

    }
}
