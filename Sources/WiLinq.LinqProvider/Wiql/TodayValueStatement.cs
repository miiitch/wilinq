using System.Text;

namespace WiLinq.LinqProvider.Wiql
{
    public sealed class TodayValueStatement : ValueStatement
    {
        /// <summary>
        ///     Initializes a new instance of the TodayValue class.
        /// </summary>
        internal TodayValueStatement(int delta)
        {
            Delta = delta;
        }

        internal TodayValueStatement()
            : this(0)
        {
        }

        public int Delta { get; }

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


        public override ValueStatement Clone()
        {
            return new TodayValueStatement(Delta);
        }
    }
}