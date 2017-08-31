using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;

namespace WiLinq.LinqProvider
{
 
    /// <summary>
    /// Class used to build the query
    /// </summary>
    internal class QueryBuilder
    {
        internal static string EncodeValue(object value)
        {
            switch (value)
            {
                case int i:
                    return i.ToString("D");
                case double d:
                    return d.ToString("F");
                case string s:
                    return "'"+s.Replace("'", "''")+"'";
                case bool b:
                    return b ? "true" : "false";          
                case DateTime dt:
                    return $"'"+dt.ToString("O")+"'";
                default:
                    throw new InvalidOperationException($"Type {value.GetType().FullName} not supported as value");
            }
        }

        private const string MACRO_FORMAT = "P{0}";
        private const string DEFAULT_COLUMN = "[System.Id]";

        private readonly List<string> _selectFieldList = null;
        private readonly List<string> _whereList = new List<string>();
        private readonly List<string> _orderbyList = new List<string>();
        private readonly QueryType _queryType;
        private QueryLinkMode _queryLinkMode;
        

        private TPCQuery _query;
     
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
            if (string.IsNullOrEmpty(whereClause))
            {
                throw new ArgumentException("whereClause is null or empty.", nameof(whereClause));
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
            if (string.IsNullOrEmpty(field))
            {
                throw new ArgumentException("field is null or empty.", nameof(field));
            }

            _orderbyList.Add(field + (isAscending ? " asc" : " desc"));
        }

     
        /// <summary>
        /// Builds the query.
        /// </summary>
        /// <param name="tpc">The server.</param>
        /// <param name="projectName">The project name</param>
        /// <param name="asOf">As of.</param>
        /// <returns></returns>
        public TPCQuery BuildQuery(WorkItemTrackingHttpClient tpc, string projectName, DateTime? asOf)
        {            
            if (_query != null)
            {
                return _query;
            }
            var builder = new StringBuilder();
        

            builder.Append("SELECT ");
            if (_selectFieldList == null)
            {
                builder.Append(DEFAULT_COLUMN);
            }
            else
            {
                builder.AppendFormat(string.Join(",", _selectFieldList.ToArray()));
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

            if (!string.IsNullOrEmpty(projectName))
            {
                _whereList.Add($"{SystemField.Project} = {EncodeValue(projectName)}");
            }

            if (_whereList.Count > 0)
            {
                builder.Append("WHERE ");
                builder.Append("(");
                builder.Append(string.Join(") AND (",_whereList.ToArray()));
                builder.AppendLine(")");                
            }

            if (_orderbyList.Count > 0)
            {                
                builder.Append(" ORDER BY ");
                builder.AppendLine(string.Join(", ", _orderbyList.ToArray()));
            }
            if (asOf.HasValue)
            {
                builder.AppendFormat(" ASOF {0}", QueryBuilder.EncodeValue(asOf.Value));
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

            var wiql = builder.ToString();

            _query = new TPCQuery(tpc, wiql, _queryType, projectName, null)
            {
                SelectLambda = SelectLambda
            };
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
        public TPCQuery Query => _query;


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