using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using WiLinq.LinqProvider.Extensions;

namespace WiLinq.LinqProvider
{
    public enum QueryLinkMode
    {
        MustContain,
        MayContain,
        DoesNotContain
    }
}
