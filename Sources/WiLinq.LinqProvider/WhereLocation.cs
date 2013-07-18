using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace WiLinq.LinqProvider
{
    /// <summary>
    /// Used to verify query constraints in the where part
    /// </summary>
    internal enum WhereLocation { LeftOperatorClause, RightOperatorClause, ElseWhere }
}
