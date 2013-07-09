using System;
using System.Collections.Generic;
using System.Linq;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class OrderStatement: Statement
    {
        public string Field { get; set; }
        public bool IsAscending { get; set; }

        protected internal override string ConvertToQueryValue()
        {
            return string.Format("[{0}] {1}",Field,IsAscending?"asc":"desc");
        }
    }
}
