using Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Request;
using FluentAssertions;
using HubSupplierTest.apps.Constants;
using HubSupplierTest.apps.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace HubSupplierTest.apps.Integration.Features.OperationBase
{
    internal class OperationBaseTest : IntegrationTest
    {
        private const string CREATE_SUPPLY_POINT = "YA4I6AP8SXRJ";
        private const string CREATE_SERIAL_NUMBER = "123456789012347";

        private async Task CreateRestoreIcpRequest()
        {
            LogTestCase();

            // Arrange
            // Act

            string bearerToken = AuthorizationUtils.GeneratePortalJwt(
                AuthenticationTestConstants.APPLICATION_SERVER_KEY,
                AuthorizationTestConstants.AUTHORIZED_USER_PORTAL1,
                true
            );

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, bearerToken);

            // Data
            CreateRestoreIcpRequest createRestoreIcpRequest = new()
            {
                SupplyPoint = CREATE_SUPPLY_POINT,
                SerialNumber = CREATE_SERIAL_NUMBER,
                Distributor = AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1
            };

            string payload = JsonConvert.SerializeObject(createRestoreIcpRequest);

            HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await TestClient.PostAsync(EndpointConstants.API_V1_RESTOREICPS, httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test, Order(1)]
        public async Task GetDataFromOtherDistributor()
        {
            LogTestCase();

            // Arrange
            // Act

            await CreateRestoreIcpRequest();

            string bearerToken = AuthorizationUtils.GenerateDistributorJwt(
                AuthenticationTestConstants.APPLICATION_SERVER_KEY,
                AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR2
            );

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, bearerToken);

            HttpResponseMessage response = await TestClient.GetAsync(EndpointConstants.API_V1_RESTOREICPS + "?OperationStatus=1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test, Order(2)]
        public async Task GetDataFromOtherPortal()
        {
            LogTestCase();

            // Arrange
            // Act

            string bearerToken = AuthorizationUtils.GenerateDistributorJwt(
                AuthenticationTestConstants.APPLICATION_SERVER_KEY,
                AuthorizationTestConstants.AUTHORIZED_USER_PORTAL2
            );

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, bearerToken);

            HttpResponseMessage response = await TestClient.GetAsync(EndpointConstants.API_V1_RESTOREICPS + "?OperationStatus=1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}