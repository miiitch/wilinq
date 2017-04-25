using System;
using System.Linq;

namespace WiLinq.LinqProvider.Wiql
{
    public abstract class ValueStatement : Statement
    {
        static ValueStatement()
        {
            Me = new MeValueStatement();
            Project = new ProjectValueStatement();
            Today = new TodayValueStatement();
        }

        internal ValueStatement()
        {

        }


        public abstract ValueStatement Copy();
       


        public static ValueStatement Me { get; private set; }
        public static ValueStatement Project { get; private set; }
        public static ValueStatement Today { get; private set; }

        public static ValueStatement Create(string value)
        {
            return new StringValueStatement(value);
        }

        public static ValueStatement Create(DateTime value)
        {
            return new DateValueStatement(value);
        }

        public static ValueStatement CreateToday(int delta)
        {
            if (delta == 0)
            {
                return Today;
            }
            return new TodayValueStatement(delta);
        }

        public static ValueStatement Create(int value)
        {
            return new IntegerValueStatement(value);
        }

        public static ValueStatement Create(params ValueStatement[] statements)
        {
            return new ListValueStatement(statements.ToList());
        }

        public static ValueStatement CreateParameter(string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentException("parameterName is null or empty.", "parameterName");

            if (parameterName.Equals("me",StringComparison.InvariantCultureIgnoreCase))
            {
                return Me;
            }
            if (parameterName.Equals("project",StringComparison.InvariantCultureIgnoreCase))
            {
                return Project;
            }
            if (parameterName.Equals("today", StringComparison.InvariantCultureIgnoreCase))
            {
                return Today;
            }
            return new ParameterValueStatement(parameterName);
        }


        public override string ToString()
        {
            return ConvertToQueryValue();
        }
    }
}
