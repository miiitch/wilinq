using System;
using System.Collections.Generic;
using System.Linq;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class ParameterValueStatement : ValueStatement
    {
        private string _parameterName;

        internal ParameterValueStatement(string parameterName):base()
        {
            _parameterName = parameterName;
        }
         

        protected internal override string ConvertToQueryValue()
        {
            return string.Format("@{0}", _parameterName);
        }


        public override ValueStatement Copy()
        {
            return new ParameterValueStatement(_parameterName);
        }
    }
}
