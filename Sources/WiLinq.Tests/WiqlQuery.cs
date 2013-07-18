using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using NUnit.Framework;
using WiLinq.LinqProvider;

namespace WiLinq.Tests
{
	[TestFixture]
	public class WiqlQuery
	{
		private TfsTeamProjectCollection _tpc;
		private Project _project;

		[TestFixtureSetUp]
		public void SetUp()
		{
			string collectionUrl = "http://localhost:8080/tfs/DefaultCollection";
			var teamProject = "Scrum";


			List<WorkItem> result;

			_tpc = new TfsTeamProjectCollection(new Uri(collectionUrl));

			_tpc.Authenticate();
			_project = _tpc.GetService<WorkItemStore>().Projects.Cast<Project>().First();
			
		}

		[Test]
		public void TPCQuery()
		{
			//all workitems;
			var allWIQuery = from workitem in _tpc.WorkItemSet()	
							 where workitem.Id == 3
							 select workitem;

			var result = allWIQuery.ToList();
		}

		[Test]
		public void ProjectQuery()
		{
			//all workitems;
			var projectWIQuery = from workitem in _project.WorkItemSet()
							 where workitem.Id == 3
							 select workitem;

			var result = projectWIQuery.ToList();
		}
		
		[Test]
		public void QueryWithFieldMethod()
		{
			var projectWiQuery = from workitem in _project.WorkItemSet()
								 where workitem.Title.Contains("Build")
								 && workitem.Field<string>("System.AssignedTo") == "Michel Perfetti"
								 select workitem;		

			var result = projectWiQuery.ToList();
		}

	

	}
}
