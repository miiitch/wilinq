using System;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.WebApi;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Core.WebApi;

namespace WiLinq.Tests
{
    public class TestFixtureBase
    {
        protected WorkItemTrackingHttpClient Client { get; private set; }
        protected TeamProject Project { get; private set; }

        [OneTimeSetUp]
        public virtual async Task SetUp()
        {
            var collectionUrl = Environment.GetEnvironmentVariable("WILINQ_TEST_TPCURL");

            var connection = new VssConnection(new Uri(collectionUrl), new VssClientCredentials());

            await connection.ConnectAsync();

            Client = await connection.GetClientAsync<WorkItemTrackingHttpClient>();

            var projectHttpClient = await connection.GetClientAsync<ProjectHttpClient>();

            Project = await projectHttpClient.GetProject("WiLinqTestProject");

            


        }
    }
}