using System;


namespace WiLinq.LinqProvider
{
    public static class QueryConstant
    {
        /// <summary>
        /// Matches the "@Me" field in a WIQL query
        /// </summary>
        /// <value>Never Used</value>
        public static string Me
        {
            get
            {
                throw new InvalidOperationException("This property is not intended to be used outside a LINQ To Wiql Query");
            }
        }

        /// <summary>
        /// Matches the "@Today" field in a WIQL query
        /// </summary>
        /// <value>Never Used</value>
        public static DateTime Today
        {
            get
            {
                throw new InvalidOperationException("This property is not intended to be used outside a LINQ To Wiql Query");
            }
        }
    }
}
