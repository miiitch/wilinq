using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Linq.Expressions;

namespace WiLinq.LinqProvider
{
	public class WorkItemFieldInfo
	{
		public string Name { get; set; }
		public Type Type { get; set; }


		public static WorkItemFieldInfo CreateIntField(string name)
		{
			return new WorkItemFieldInfo()
			{
				Name = name,
				Type = typeof(int)
			};
		}

		public static WorkItemFieldInfo CreateStringField(string name)
		{
			return new WorkItemFieldInfo()
			{
				Name = name,
				Type = typeof(string)
			};
		}

		internal static WorkItemFieldInfo CreateDateField(string name)
		{
			return new WorkItemFieldInfo()
			{
				Name = name,
				Type = typeof(DateTime)
			};
		}
	}
}
