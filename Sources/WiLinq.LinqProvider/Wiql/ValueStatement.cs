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

        public abstract ValueStatement Copy();

        public override string ToString() => ConvertToQueryValue();


        public static ValueStatement Me { get; }
        public static ValueStatement Project { get; }
        public static ValueStatement Today { get; }

        public static ValueStatement Create(string value) => new StringValueStatement(value);

        public static ValueStatement Create(DateTime value) => new DateValueStatement(value);

        public static ValueStatement CreateToday(int delta) => delta == 0 ? Today : new TodayValueStatement(delta);

        public static ValueStatement Create(int value) => new IntegerValueStatement(value);

        public static ValueStatement Create(params ValueStatement[] statements) => new ListValueStatement(statements.ToList());

        public static ValueStatement CreateParameter(string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentException("parameterName is null or empty.", nameof(parameterName));

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

    }
}
