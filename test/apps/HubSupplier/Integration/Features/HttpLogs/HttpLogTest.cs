using Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Request;
using Aseme.HubSupplier.HttpLogs.Domain;
using FluentAssertions;
using HubSupplierTest.apps.Constants;
using HubSupplierTest.apps.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace HubSupplierTest.apps.Integration.Features.HttpLogs
{
    public class HttpLogTest : IntegrationTest
    {
        private const string CREATE_SUPPLY_POINT = "YA4I6AP8SXRJ";
        private const string CREATE_SERIAL_NUMBER = "123456789012345";

        private const int RequestId = 1;
        private const string HttpScheme = "HTTP";
        private const string HttpMethodPost = "POST";
        private const string HttpPath = "/api/restoreicps";
        private const string HttpQueryParams = "?api-version=1";
        private const string HttpRequestHeaderHost = "Host";
        private const string HttpResponseHeaderApiSupportedVersions = "api-supported-versions";
        private const int HttpStatusCodeCreated = 201;
        private const int MinExecutionTime = 0;
        private const long RestoreIcpEntityId = 1L;

        [Test, Order(1)]
        public async Task LogHttpPost()
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
            HttpResponseMessage response = await TestClient.PostAsync(EndpointConstants.API_RESTOREICPS + "?api-version=1", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test, Order(2)]
        public async Task GetLoggedHttpPost()
        {
            LogTestCase();

            HttpLog httpLog = await IGetHttpLogService.GetAsync(RequestId);
            httpLog.ReceivedDateTime.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromHours(2));
            httpLog.IpAddress.Should().Be(string.Empty);
            httpLog.Scheme.Should().Be(HttpScheme);
            httpLog.HttpMethod.Should().Be(HttpMethodPost);
            httpLog.HttpPath.Should().Be(HttpPath);
            httpLog.HttpQueryParams.Should().Be(HttpQueryParams);
            httpLog.HttpRequestHeaders.Should().Contain(HttpRequestHeaderHost);
            httpLog.HttpRequestBody.Should().Contain(CREATE_SUPPLY_POINT);
            httpLog.HttpResponseHeaders.Should().Contain(HttpResponseHeaderApiSupportedVersions);
            httpLog.HttpResponseBody.Should().Contain(CREATE_SUPPLY_POINT);
            httpLog.HttpStatusCode.Should().Be(HttpStatusCodeCreated);
            httpLog.ExecutionTime.Should().BeGreaterThan(MinExecutionTime);
            httpLog.EntityId.Should().Be(RestoreIcpEntityId);
        }
    }
}