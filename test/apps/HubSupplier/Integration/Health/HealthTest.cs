using FluentAssertions;
using HubSupplierTest.apps.Constants;
using NUnit.Framework;
using System.Net;

namespace HubSupplierTest.apps.Integration.Health
{
    public class HealthTest : IntegrationTest
    {
        [Test]
        public async Task HealthCheck()
        {
            LogTestCase();

            HttpResponseMessage response = await TestClient.GetAsync(EndpointConstants.API_HEALTH);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}