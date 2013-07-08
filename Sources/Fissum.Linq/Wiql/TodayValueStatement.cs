using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class TodayValueStatement : ValueStatement
    {
        public int Delta { get; private set; }
        /// <summary>
        /// Initializes a new instance of the TodayValue class.
        /// </summary>
        internal TodayValueStatement(int delta)
        {
            Delta = delta;
        }

        internal TodayValueStatement()
            : this(0)
        {

        }

        protected internal override string ConvertToQueryValue()
        {
            var ret = new StringBuilder("@today");

            if (Delta > 0)
            {
                ret.AppendFormat("+{0}", Delta);
            }
            else if (Delta < 0)
            {
                ret.AppendFormat("{0}", Delta);
            }

            return ret.ToString();
        }


        public override ValueStatement Copy()
        {
            return new TodayValueStatement(Delta);
        }

    }
}
