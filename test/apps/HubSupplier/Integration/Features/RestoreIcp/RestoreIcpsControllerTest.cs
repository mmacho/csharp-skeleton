using Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Request;
using Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Response;
using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.HubSupplier.Shared.Domain.Operation;
using Aseme.Shared.Infrastructure.Http.Response;
using FluentAssertions;
using HubSupplierTest.apps.Constants;
using HubSupplierTest.apps.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace HubSupplierTest.apps.Integration.Features.RestoreIcp
{
    public class RestoreIcpsControllerTest : IntegrationTest
    {
        private const string CREATE_SUPPLY_POINT = "YA4I6AP8SXRJ";
        private const string CREATE_SERIAL_NUMBER = "123456789012345";

        private const string UPDATE_SUPPLY_POINT = "1A4I6AP8SXRV";
        private const string UPDATE_SERIAL_NUMBER = "323456789012348";
        private const string UPDATE_DESCRIPTION = "Descrìption";

        private byte[] restoreIcpVersion = Array.Empty<byte>();

        [Test, Order(1)]
        public async Task CreateRestoreIcpWithDistributorToken()
        {
            LogTestCase();

            // Arrange
            // Act

            string bearerToken = AuthorizationUtils.GenerateDistributorJwt(
                AuthenticationTestConstants.APPLICATION_SERVER_KEY,
                AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1
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
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Test, Order(2)]
        public async Task CreateRestoreIcpWithPortalToken()
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

            string content = await response.Content.ReadAsStringAsync();

            Response<RestoreIcpResponse>? restoreIcpResponse = JsonConvert.DeserializeObject<Response<RestoreIcpResponse>>(content);

            if (restoreIcpResponse == null)
            {
                throw new Exception("Response is null");
            }

            restoreIcpResponse.Data.Id.Should().Be(1);
            restoreIcpResponse.Data.Version.Should().NotBeNullOrEmpty();
            restoreIcpResponse.Data.SupplyPoint.Should().Be(CREATE_SUPPLY_POINT);
            restoreIcpResponse.Data.SerialNumber.Should().Be(CREATE_SERIAL_NUMBER);
            restoreIcpResponse.Data.Distributor.Should().Be(AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1);

            restoreIcpResponse.Succeeded.Should().BeTrue();

            // Update version
            restoreIcpVersion = restoreIcpResponse.Data.Version;
        }

        [Test, Order(3)]
        public async Task SearchRestoreIcp()
        {
            LogTestCase();

            // Arrange
            // Act

            string bearerToken = AuthorizationUtils.GenerateDistributorJwt(
                AuthenticationTestConstants.APPLICATION_SERVER_KEY,
                AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1
            );

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, bearerToken);

            HttpResponseMessage response = await TestClient.GetAsync(EndpointConstants.API_V1_RESTOREICPS + "?OperationStatus=1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            string content = await response.Content.ReadAsStringAsync();

            PagedResponse<RestoreIcpResponse>? pagedResponse = JsonConvert.DeserializeObject<PagedResponse<RestoreIcpResponse>>(content);

            if (pagedResponse == null)
            {
                throw new Exception("Response is null");
            }

            pagedResponse.PageNumber.Should().Be(1);
            pagedResponse.PageSize.Should().Be(1);
            pagedResponse.TotalPages.Should().Be(1);
            pagedResponse.TotalCount.Should().Be(1);
            pagedResponse.HasPreviousPage.Should().Be(false);
            pagedResponse.HasNextPage.Should().Be(false);
            pagedResponse.FirstPage.Should().NotBe(null);
            pagedResponse.LastPage.Should().NotBe(null);

            pagedResponse.Data.Count.Should().Be(1);
            pagedResponse.Data[0].SupplyPoint.Should().Be(CREATE_SUPPLY_POINT);
            pagedResponse.Data[0].SerialNumber.Should().Be(CREATE_SERIAL_NUMBER);
            pagedResponse.Data[0].Distributor.Should().Be(AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1);
            pagedResponse.Data[0].OperationStatus.Should().Be(OperationStatusType.RECEIVED);
            pagedResponse.Data[0].Id.Should().Be(1);
            pagedResponse.Data[0].Version.Should().NotBeNullOrEmpty();
            pagedResponse.Data[0].CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromHours(2));
            pagedResponse.Data[0].LastModifiedDate.Should().Be(DateTime.Parse("0001-01-01T00:00:00"));

            pagedResponse.Succeeded.Should().BeTrue();
        }

        [Test, Order(4)]
        public async Task SearchRestoreIcpWithPaging()
        {
            LogTestCase();

            // Arrange
            // Act

            string bearerToken = AuthorizationUtils.GenerateDistributorJwt(
                AuthenticationTestConstants.APPLICATION_SERVER_KEY,
                AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1
            );

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, bearerToken);

            HttpResponseMessage response = await TestClient.GetAsync(EndpointConstants.API_V1_RESTOREICPS + "?OperationStatus=1&PageNumber=1&PageSize=1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            string content = await response.Content.ReadAsStringAsync();

            PagedResponse<RestoreIcpResponse>? pagedResponse = JsonConvert.DeserializeObject<PagedResponse<RestoreIcpResponse>>(content);

            if (pagedResponse == null)
            {
                throw new Exception("Response is null");
            }

            pagedResponse.PageNumber.Should().Be(1);
            pagedResponse.PageSize.Should().Be(1);
            pagedResponse.TotalPages.Should().Be(1);
            pagedResponse.TotalCount.Should().Be(1);
            pagedResponse.HasPreviousPage.Should().Be(false);
            pagedResponse.HasNextPage.Should().Be(false);
            pagedResponse.FirstPage.Should().NotBe(null);
            pagedResponse.LastPage.Should().NotBe(null);

            pagedResponse.Data.Count.Should().Be(1);
            pagedResponse.Data[0].SupplyPoint.Should().Be(CREATE_SUPPLY_POINT);
            pagedResponse.Data[0].SerialNumber.Should().Be(CREATE_SERIAL_NUMBER);
            pagedResponse.Data[0].Distributor.Should().Be(AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1);
            pagedResponse.Data[0].OperationStatus.Should().Be(OperationStatusType.RECEIVED);
            pagedResponse.Data[0].Id.Should().Be(1);
            pagedResponse.Data[0].Version.Should().NotBeNullOrEmpty();
            pagedResponse.Data[0].CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromHours(2));
            pagedResponse.Data[0].LastModifiedDate.Should().Be(DateTime.Parse("0001-01-01T00:00:00"));

            pagedResponse.Succeeded.Should().BeTrue();
        }

        [Test, Order(5)]
        public async Task UpdateRestoreIcp()
        {
            LogTestCase();

            // Arrange
            // Act

            string bearerToken = AuthorizationUtils.GenerateDistributorJwt(
                AuthenticationTestConstants.APPLICATION_SERVER_KEY,
                AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1
            );

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, bearerToken);

            // Data
            UpdateRestoreIcpRequest updateRestoreIcpRequest = new()
            {
                SupplyPoint = UPDATE_SUPPLY_POINT,
                SerialNumber = UPDATE_SERIAL_NUMBER,
                Distributor = AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1,
                OperationStatus = OperationStatusType.RUN,
                Version = restoreIcpVersion,

                RestoreIcpDetails = new UpdateRestoreIcpDetailsRequest
                {
                    RestoreIcpStatus = RestoreIcpStatusType.KO,
                    Description = UPDATE_DESCRIPTION,
                    ExecutionDate = DateTime.UtcNow
                }
            };

            string payload = JsonConvert.SerializeObject(updateRestoreIcpRequest);

            HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await TestClient.PutAsync(EndpointConstants.API_V1_RESTOREICPS + "/1", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            string content = await response.Content.ReadAsStringAsync();

            Response<RestoreIcpResponse>? restoreIcpResponse = JsonConvert.DeserializeObject<Response<RestoreIcpResponse>>(content);

            if (restoreIcpResponse == null)
            {
                throw new Exception("Response is null");
            }

            restoreIcpResponse.Data.Id.Should().Be(1);
            restoreIcpResponse.Data.SupplyPoint.Should().Be(UPDATE_SUPPLY_POINT);
            restoreIcpResponse.Data.SerialNumber.Should().Be(UPDATE_SERIAL_NUMBER);
            restoreIcpResponse.Data.Distributor.Should().Be(AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1);
            restoreIcpResponse.Data.OperationStatus.Should().Be(OperationStatusType.RUN);

            Assert.NotNull(restoreIcpResponse.Data?.RestoreIcpDetails);

            restoreIcpResponse.Data?.RestoreIcpDetails?.RestoreIcpStatus.Should().Be(RestoreIcpStatusType.KO);
            restoreIcpResponse.Data?.RestoreIcpDetails?.Description.Should().Be(UPDATE_DESCRIPTION);
            restoreIcpResponse.Data?.RestoreIcpDetails?.ExecutionDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromHours(2));

            restoreIcpResponse.Succeeded.Should().BeTrue();

            // Update version
            restoreIcpVersion = restoreIcpResponse?.Data?.Version ?? Array.Empty<byte>();
        }

        [Test, Order(6)]
        public async Task UpdateRestoreIcpDetails()
        {
            LogTestCase();

            // Arrange
            // Act

            string bearerToken = AuthorizationUtils.GenerateDistributorJwt(
                AuthenticationTestConstants.APPLICATION_SERVER_KEY,
                AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1
            );

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, bearerToken);

            // Data
            UpdateRestoreIcpRequest updateRestoreIcpRequest = new()
            {
                SupplyPoint = UPDATE_SUPPLY_POINT,
                SerialNumber = UPDATE_SERIAL_NUMBER,
                Distributor = AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1,
                OperationStatus = OperationStatusType.RUN,
                Version = restoreIcpVersion,

                RestoreIcpDetails = new UpdateRestoreIcpDetailsRequest
                {
                    RestoreIcpStatus = RestoreIcpStatusType.OK,
                    Description = UPDATE_DESCRIPTION,
                    ExecutionDate = DateTime.UtcNow
                }
            };

            string payload = JsonConvert.SerializeObject(updateRestoreIcpRequest);

            HttpContent httpContent = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await TestClient.PutAsync(EndpointConstants.API_V1_RESTOREICPS + "/1", httpContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            string content = await response.Content.ReadAsStringAsync();

            Response<RestoreIcpResponse>? restoreIcpResponse = JsonConvert.DeserializeObject<Response<RestoreIcpResponse>>(content);

            if (restoreIcpResponse == null)
            {
                throw new Exception("Response is null");
            }

            restoreIcpResponse.Data.Id.Should().Be(1);
            restoreIcpResponse.Data.SupplyPoint.Should().Be(UPDATE_SUPPLY_POINT);
            restoreIcpResponse.Data.SerialNumber.Should().Be(UPDATE_SERIAL_NUMBER);
            restoreIcpResponse.Data.Distributor.Should().Be(AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1);
            restoreIcpResponse.Data.OperationStatus.Should().Be(OperationStatusType.RUN);

            Assert.NotNull(restoreIcpResponse.Data?.RestoreIcpDetails);

            restoreIcpResponse.Data?.RestoreIcpDetails?.RestoreIcpStatus.Should().Be(RestoreIcpStatusType.OK);
            restoreIcpResponse.Data?.RestoreIcpDetails?.Description.Should().Be(UPDATE_DESCRIPTION);
            restoreIcpResponse.Data?.RestoreIcpDetails?.ExecutionDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromHours(2));

            // Update version
            restoreIcpResponse.Succeeded.Should().BeTrue();
        }

        [Test, Order(7)]
        public async Task GetRestoreIcp()
        {
            LogTestCase();

            // Arrange
            // Act

            string bearerToken = AuthorizationUtils.GenerateDistributorJwt(
                AuthenticationTestConstants.APPLICATION_SERVER_KEY,
                AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1
            );

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, bearerToken);

            HttpResponseMessage response = await TestClient.GetAsync(EndpointConstants.API_V1_RESTOREICPS + "/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            string content = await response.Content.ReadAsStringAsync();

            Response<RestoreIcpResponse>? restoreIcpResponse = JsonConvert.DeserializeObject<Response<RestoreIcpResponse>>(content);

            if (restoreIcpResponse == null)
            {
                throw new Exception("Response is null");
            }

            restoreIcpResponse.Data.SupplyPoint.Should().Be(UPDATE_SUPPLY_POINT);
            restoreIcpResponse.Data.SerialNumber.Should().Be(UPDATE_SERIAL_NUMBER);
            restoreIcpResponse.Data.Distributor.Should().Be(AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1);
            restoreIcpResponse.Data.OperationStatus.Should().Be(OperationStatusType.RUN);
            restoreIcpResponse.Data.Id.Should().Be(1);
            restoreIcpResponse.Data.Version.Should().NotBeNullOrEmpty();
            restoreIcpResponse.Data.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromHours(2));
            restoreIcpResponse.Data.LastModifiedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromHours(2));

            Assert.NotNull(restoreIcpResponse.Data?.RestoreIcpDetails);

            restoreIcpResponse.Data?.RestoreIcpDetails?.RestoreIcpStatus.Should().Be(RestoreIcpStatusType.OK);
            restoreIcpResponse.Data?.RestoreIcpDetails?.Description.Should().Be(UPDATE_DESCRIPTION);
            restoreIcpResponse.Data?.RestoreIcpDetails?.ExecutionDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromHours(2));

            restoreIcpResponse.Succeeded.Should().BeTrue();
        }

        [Test, Order(8)]
        public async Task DeleteRestoreIcp()
        {
            LogTestCase();

            // Arrange
            // Act

            string bearerToken = AuthorizationUtils.GenerateDistributorJwt(
                AuthenticationTestConstants.APPLICATION_SERVER_KEY,
                AuthorizationTestConstants.AUTHORIZED_USER_DISTRIBUTOR1
            );

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, bearerToken);

            HttpResponseMessage response = await TestClient.DeleteAsync(EndpointConstants.API_V1_RESTOREICPS + "/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}