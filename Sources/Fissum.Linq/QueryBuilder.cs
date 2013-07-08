using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace WiLinq.LinqProvider
{
 
    /// <summary>
    /// Class used to build the query
    /// </summary>
    internal class QueryBuilder
    {
        private const string MACROFORMAT = "P{0}";
        private const string DEFAULTCOLUMN = "[System.Id]";

        private List<string> _selectFieldList = null;
        private List<string> _whereList = new List<string>();
        private List<string> _orderbyList = new List<string>();
        private int _macroIndex = 0;
        private QueryType _queryType;
        private QueryLinkMode _queryLinkMode;
        

        private TPCQuery _query;
        

        private Dictionary<string, object> _macroDictionnary = new Dictionary<string, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder"/> class.
        /// </summary>
        public QueryBuilder(QueryType queryType)
        {
            _queryType = queryType;
            _queryLinkMode = QueryLinkMode.MustContain;
        }

        /// <summary>
        /// Adds a where clause in the query
        /// </summary>
        /// <param name="whereClause">The where clause.</param>
        public void AddWhereClause(string whereClause)
        {
            if (String.IsNullOrEmpty(whereClause))
            {
                throw new ArgumentException("whereClause is null or empty.", "whereClause");
            }
            _whereList.Add(whereClause);
        }

        /// <summary>
        /// Adds an order clause in the query
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="isAscending">if set to <c>true</c> [is ascending].</param>
        public void AddOrderClause(string field, bool isAscending)
        {
            if (String.IsNullOrEmpty(field))
            {
                throw new ArgumentException("field is null or empty.", "field");
            }

            _orderbyList.Add(field + (isAscending ? " asc" : " desc"));
        }

        /// <summary>
        /// Generates a variable name and associate the given value
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public string GenerateMacro(object value)
        {
            string macroName = String.Format(MACROFORMAT,_macroIndex);
            _macroIndex++;
            _macroDictionnary.Add(macroName, value);
            return "@" + macroName;
        }

        /// <summary>
        /// Builds the query.
        /// </summary>
        /// <param name="tpc">The server.</param>
        /// <param name="project">The project.</param>
        /// <param name="asOf">As of.</param>
        /// <returns></returns>
        public TPCQuery BuildQuery(TfsTeamProjectCollection tpc, Project project, DateTime? asOf)
        {            
            if (_query != null)
            {
                return _query;
            }
            StringBuilder builder = new StringBuilder();
            
            if (project != null)
            {
                _macroDictionnary.Add("project", project.Name);
            }

            builder.Append("SELECT ");
            if (_selectFieldList == null)
            {
                builder.Append(DEFAULTCOLUMN);
            }
            else
            {
                builder.AppendFormat(String.Join(",", _selectFieldList.ToArray()));
            }
            builder.AppendLine();
            if (_queryType == QueryType.WorkItem)
            {
                builder.AppendLine("FROM WORKITEMS ");
            }
            else if (_queryType == QueryType.Link)
            {
                builder.AppendLine("FROM WORKITEMLINKS");
            }
            if (_whereList.Count > 0)
            {
                builder.Append("WHERE ");
                builder.Append("(");
                builder.Append(String.Join(") AND (",_whereList.ToArray()));
                builder.AppendLine(")");                
            }

            if (_orderbyList.Count > 0)
            {                
                builder.Append(" ORDER BY ");
                builder.AppendLine(String.Join(", ", _orderbyList.ToArray()));
            }
            if (asOf.HasValue)
            {
                builder.AppendFormat(" ASOF {0}", GenerateMacro(asOf.Value));
            }

            if (_queryType == QueryType.Link)
            {
                switch (_queryLinkMode)
                {
                    case QueryLinkMode.DoesNotContain:
                        builder.Append(" mode(doesnotcontain)");
                        break;
                    case QueryLinkMode.MayContain:
                        builder.Append(" mode(maycontain)");
                        break;
                    case QueryLinkMode.MustContain:
                        builder.Append(" mode(mustcontain)");
                        break;
                    default:
                        throw new InvalidOperationException("Invalid query link mode");
                }
            }

            string wiql = builder.ToString();

            _query = new TPCQuery(tpc, wiql, _macroDictionnary, _queryType);
            return _query;
        }

        /// <summary>
        /// Gets or sets the select method. This is used to generate the final list of work item
        /// </summary>
        /// <value>The select method.</value>
        public MethodInfo SelectMethod
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the select lambda. This is used to generate the final list of work item
        /// </summary>
        /// <value>The select lambda.</value>
        public LambdaExpression SelectLambda
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>The query.</value>
        public TPCQuery Query
        {
            get
            {
                return _query;
            }          
        }


        internal void AddQueryLinkMode(QueryLinkMode mode)
        {
            if (_queryType != QueryType.Link)
            {
                throw new InvalidOperationException("This method is only used in case of Link query");
            }

            _queryLinkMode = mode;
        }
    }

}