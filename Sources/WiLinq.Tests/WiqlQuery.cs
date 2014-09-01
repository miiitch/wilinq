using System;
using System.Linq;
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
            var collectionUrl = Environment.GetEnvironmentVariable("WILINQ_TEST_TPCURL");

            if (string.IsNullOrWhiteSpace(collectionUrl))
            {
                collectionUrl = "http://localhost:8080/tfs/DefaultCollection";
            }



            _tpc = new TfsTeamProjectCollection(new Uri(collectionUrl));

            _tpc.Authenticate();

            var projectName = Environment.GetEnvironmentVariable("WILINQ_TEST_PROJECTNAME");

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (string.IsNullOrWhiteSpace(projectName))
            {
                _project = _tpc.GetService<WorkItemStore>().Projects.Cast<Project>().First();
            }
            else
            {

                _project = _tpc.GetService<WorkItemStore>().Projects.Cast<Project>().First(_ => _.Name == projectName);
            }



        }

        [Test]
        // ReSharper disable once InconsistentNaming
        public void TPCQuery()
        {
            //all workitems;
            var allWiQuery = from workitem in _tpc.WorkItemSet()
                             where workitem.Id == 3
                             select workitem;

            // ReSharper disable once UnusedVariable
            var result = allWiQuery.ToList();
        }

        [Test]
        public void ProjectQueryAllWorkitems()
        {
            //all workitems;
            var projectWiQuery = from workitem in _project.WorkItemSet()
                                 select workitem;

            // ReSharper disable once UnusedVariable
            var result = projectWiQuery.ToList();
        }

        [Test]
        public void ProjectQuery()
        {
            //all workitems;
            var projectWiQuery = from workitem in _project.WorkItemSet()
                                 where workitem.Id == 3
                                 select workitem;

            // ReSharper disable once UnusedVariable
            var result = projectWiQuery.ToList();
        }

        [Test]
        public void QueryWithFieldMethod()
        {
            var projectWiQuery = from workitem in _project.WorkItemSet()
                                 where workitem.Title.Contains("Build")
                                 && workitem.Field<string>("System.AssignedTo") == "Michel Perfetti"
                                 select workitem;
            // ReSharper disable once UnusedVariable
            var result = projectWiQuery.ToList();
        }



    }
}
