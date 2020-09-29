using System;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using NUnit.Framework;

namespace WiLinq.Tests
{
    public class TestFixtureBase
    {
        protected WorkItemTrackingHttpClient Client { get; private set; }
        protected TeamProject Project { get; private set; }

        protected string TestSessionId { get; set; }


        [OneTimeSetUp]
        public virtual async Task SetUp()
        {
            var collectionUrl = Environment.GetEnvironmentVariable("WILINQ_TEST_TPCURL");
            if (string.IsNullOrWhiteSpace(collectionUrl))
            {
                throw new InvalidOperationException("Environment variable 'WILINQ_TEST_TPCURL' is missing");
            }

            var projectName = Environment.GetEnvironmentVariable("WILINQ_TEST_PROJECT");
            var personnalAccessToken = Environment.GetEnvironmentVariable("WILINQ_TEST_PAT");
            VssCredentials vssCredentials;
            if (string.IsNullOrWhiteSpace(personnalAccessToken))
            {
                vssCredentials = new VssClientCredentials
                {
                    Storage = new VssClientCredentialStorage()
                };
            }
            else
            {
                vssCredentials = new VssBasicCredential(string.Empty, personnalAccessToken);
            }

            var connection = new VssConnection(new Uri(collectionUrl), vssCredentials);

            await connection.ConnectAsync();

            Client = await connection.GetClientAsync<WorkItemTrackingHttpClient>();

            var projectHttpClient = await connection.GetClientAsync<ProjectHttpClient>();

            Project = await projectHttpClient.GetProject(projectName);

            TestSessionId = Guid.NewGuid().ToString();
        }
    }
}