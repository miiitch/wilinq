using System;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.WebApi;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using System.Threading.Tasks;

namespace WiLinq.Tests
{
    public class TestFixtureBase
    {
        protected WorkItemTrackingHttpClient Client { get; private set; }
        protected Project Project { get; private set; }

        [OneTimeSetUp]
        public async Task SetUp()
        {
            var collectionUrl = Environment.GetEnvironmentVariable("WILINQ_TEST_TPCURL");

            var connection = new VssConnection(new Uri(collectionUrl), new VssClientCredentials());

            await connection.ConnectAsync();



            Client = await connection.GetClientAsync<WorkItemTrackingHttpClient>();



        }
    }
}