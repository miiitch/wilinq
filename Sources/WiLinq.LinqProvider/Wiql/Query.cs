using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace WiLinq.LinqProvider.Wiql
{
	public sealed class Query
	{

		public List<string> Fields { get; private set; }
		public List<WhereStatement> WhereStatements { get; private set; }
		public QueryMode Mode { get; private set; }
		public List<OrderStatement> OrderStatements { get; private set; }


		public Query(QueryMode mode)
		{
			Fields = new List<string>();
			WhereStatements = new List<WhereStatement>();
			OrderStatements = new List<OrderStatement>();
			Mode = mode;
		}

		public Query()
			: this(QueryMode.Default)
		{

		}
#if false
		public List<QueryIssue> AnalyseQuery()
		{
			return AnalyseQuery(null);
		}

		public List<QueryIssue> AnalyseQuery(Project project)
		{
			var result = new List<QueryIssue>();


			return result;
		}
#endif
		public string ToQuery()
		{
			StringBuilder builder = new StringBuilder();


			builder.Append("select ");

			string fields = string.Join(", ", Fields.Select(field => string.Format("[{0}]", field)).ToArray());

			builder.Append(fields);
			builder.Append(" from ");

			if (Mode == QueryMode.Default)
			{
				builder.Append("WorkItems");
			}
			else
			{
				builder.Append("WorkItemLinks");
			}

			var whereStringStatements = WhereStatements.Select(statement => string.Format(" where {0} ", statement.ConvertToQueryValue()));
			foreach (var st in whereStringStatements)
			{
				builder.Append(st);
			}

			if (OrderStatements.Count > 0)
			{
				builder.Append(" order by ");
				var orders = OrderStatements.Select(ost => ost.ConvertToQueryValue()).ToArray();
				builder.Append(string.Join(", ", orders.ToArray()));
			}


			return null;
		}

#if false
		public Predicate<WorkItem> GeneratePredicate()
		{
			throw new NotImplementedException();
		}
#endif
		public Query Copy()
		{
			var visitor = new QueryVisitor();
			return visitor.Visit(this);
		}
	}

}