using System;
using System.Collections.Generic;
using System.Linq;

namespace WiLinq.LinqProvider.Wiql
{
    public abstract class Statement
    {

        protected internal abstract string ConvertToQueryValue();

    }
}
