using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiLinq.LinqProvider.Wiql
{
	public sealed class Query
	{

		public List<string> Fields { get; }
		public List<WhereStatement> WhereStatements { get; }
		public QueryMode Mode { get; }
		public List<OrderStatement> OrderStatements { get; }


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
			var builder = new StringBuilder();


			builder.Append("select ");

			var fields = string.Join(", ", Fields.Select(field => $"[{field}]").ToArray());

			builder.Append(fields);
			builder.Append(" from ");

		    builder.Append(Mode == QueryMode.Default ? "WorkItems" : "WorkItemLinks");

		    var whereStringStatements = WhereStatements.Select(statement => $" where {statement.ConvertToQueryValue()} ");
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