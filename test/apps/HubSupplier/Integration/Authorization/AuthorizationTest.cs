using Aseme.HubSupplier.Shared.Infrastructure.Constants;
using Aseme.Shared.Domain;
using Aseme.Shared.Infrastructure.Http.Response;
using Aseme.Shared.Infrastructure.Http.Response.Error;
using FluentAssertions;
using HubSupplierTest.apps.Constants;
using HubSupplierTest.apps.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;

namespace HubSupplierTest.apps.Integration.Authorization
{
    public class AuthorizationTest : IntegrationTest
    {
        [Test]
        public async Task TokenWithoutUser()
        {
            LogTestCase();

            string bearerToken = AuthorizationUtils.GenerateDistributorJwt(
                AuthenticationTestConstants.APPLICATION_SERVER_KEY,
                AuthorizationTestConstants.MISSING_USERNAME
            );

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, bearerToken);

            HttpResponseMessage response = await TestClient.GetAsync(EndpointConstants.API_V1_TEST);

            string content = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            ErrorResponse<CustomProblemDetails>? errorResponse = JsonConvert.DeserializeObject<ErrorResponse<CustomProblemDetails>>(content);

            CustomProblemDetails? customProblemDetails = errorResponse?.Error;
            Assert.IsNotNull(customProblemDetails);

            customProblemDetails?.Error.Code.Should().Be(ErrorCode.INVALID_TOKEN);
            customProblemDetails?.Error.Message.Should().Contain(AuthorizationConstants.USER_NOT_FOUND_MESSAGE);
        }

        [Test]
        public async Task TokenWithUnauthorizedUser()
        {
            LogTestCase();

            string bearerToken = AuthorizationUtils.GenerateDistributorJwt(
                AuthenticationTestConstants.APPLICATION_SERVER_KEY,
                AuthorizationTestConstants.UNAUTHORIZED_USERNAME
            );

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, bearerToken);

            HttpResponseMessage response = await TestClient.GetAsync(EndpointConstants.API_V1_TEST);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string content = await response.Content.ReadAsStringAsync();

            ErrorResponse<CustomProblemDetails>? errorResponse = JsonConvert.DeserializeObject<ErrorResponse<CustomProblemDetails>>(content);

            CustomProblemDetails? customProblemDetails = errorResponse?.Error;
            Assert.IsNotNull(customProblemDetails);

            customProblemDetails?.Error.Code.Should().Be(ErrorCode.UNAUTHORIZED);
            customProblemDetails?.Error.Message.Should().Be(AuthorizationConstants.USER_NOT_AUTHORIZED);
        }

        [Test]
        public async Task TokenWithAuthorizedUser()
        {
            LogTestCase();

            string bearerToken = AuthorizationUtils.GenerateDistributorJwt(
                AuthenticationTestConstants.APPLICATION_SERVER_KEY,
                AuthorizationTestConstants.AUTHORIZED_USER_PORTAL1
            );

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, bearerToken);

            HttpResponseMessage response = await TestClient.GetAsync(EndpointConstants.API_V1_TEST);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}