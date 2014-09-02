using System;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using NUnit.Framework;

namespace WiLinq.Tests
{
    public class TestFixtureBase
    {
        protected TfsTeamProjectCollection TPC { get; private set; }
        protected Project Project { get; private set; }

        [TestFixtureSetUp]
        public virtual void SetUp()
        {
            var collectionUrl = Environment.GetEnvironmentVariable("WILINQ_TEST_TPCURL");

            if (string.IsNullOrWhiteSpace(collectionUrl))
            {
                collectionUrl = "http://localhost:8080/tfs/DefaultCollection";
            }



            TPC = new TfsTeamProjectCollection(new Uri(collectionUrl));

            TPC.Authenticate();

            var projectName = Environment.GetEnvironmentVariable("WILINQ_TEST_PROJECTNAME");

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (string.IsNullOrWhiteSpace(projectName))
            {
                Project = TPC.GetService<WorkItemStore>().Projects.Cast<Project>().First();
            }
            else
            {

                Project = TPC.GetService<WorkItemStore>().Projects.Cast<Project>().First(_ => _.Name == projectName);
            }



        }
    }
}