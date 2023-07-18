using Aseme.HubSupplier.Shared.Infrastructure.Constants;
using Aseme.Shared.Domain.Exceptions;
using Aseme.Shared.Infrastructure.Http.Response;
using Aseme.Shared.Infrastructure.Http.Response.Error;
using FluentAssertions;
using HubSupplierTest.apps.Constants;
using HubSupplierTest.apps.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;

namespace HubSupplierTest.apps.Integration.Authentication
{
    public class AuthenticationTest : IntegrationTest
    {
        [Test]
        public async Task NoToken()
        {
            LogTestCase();

            TestClient.DefaultRequestHeaders.Authorization = null;

            HttpResponseMessage response = await TestClient.GetAsync(EndpointConstants.API_V1_TEST);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string content = await response.Content.ReadAsStringAsync();

            ErrorResponse<CustomProblemDetails>? errorResponse = JsonConvert.DeserializeObject<ErrorResponse<CustomProblemDetails>>(content);

            CustomProblemDetails? customProblemDetails = errorResponse?.Error;
            Assert.IsNotNull(customProblemDetails);

            customProblemDetails?.Error.Code.Should().Be(ErrorCode.UNAUTHORIZED);
            customProblemDetails?.Error.Message.Should().Be(AuthenticationConstants.AUTHORIZATION_HEADER_MISSING_MESSAGE);
        }

        [Test]
        public async Task TokenWithInvalidSignature()
        {
            LogTestCase();

            string bearerToken = AuthorizationUtils.GeneratePortalJwt(
                AuthenticationTestConstants.INVALID_APPLICATION_SERVER_KEY,
                AuthorizationTestConstants.AUTHORIZED_USER_PORTAL1,
                true
            );

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, bearerToken);

            HttpResponseMessage response = await TestClient.GetAsync(EndpointConstants.API_V1_TEST);
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            string content = await response.Content.ReadAsStringAsync();

            ErrorResponse<CustomProblemDetails>? errorResponse = JsonConvert.DeserializeObject<ErrorResponse<CustomProblemDetails>>(content);

            CustomProblemDetails? customProblemDetails = errorResponse?.Error;
            Assert.IsNotNull(customProblemDetails);

            customProblemDetails?.Error.Code.Should().Be(ErrorCode.INVALID_TOKEN);
            customProblemDetails?.Error.Message.Should().Be(AuthenticationConstants.INVALID_TOKEN_MESSAGE);
        }

        [Test]
        public async Task TokenWithoutExpiration()
        {
            LogTestCase();

            string bearerToken = AuthorizationUtils.GeneratePortalJwt(
                AuthenticationTestConstants.APPLICATION_SERVER_KEY,
                AuthorizationTestConstants.AUTHORIZED_USER_PORTAL1,
                false
            );

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, bearerToken);

            HttpResponseMessage response = await TestClient.GetAsync(EndpointConstants.API_V1_TEST);
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            string content = await response.Content.ReadAsStringAsync();

            ErrorResponse<CustomProblemDetails>? errorResponse = JsonConvert.DeserializeObject<ErrorResponse<CustomProblemDetails>>(content);

            CustomProblemDetails? customProblemDetails = errorResponse?.Error;
            Assert.IsNotNull(customProblemDetails);

            customProblemDetails?.Error.Code.Should().Be(ErrorCode.INVALID_TOKEN);
            customProblemDetails?.Error.Message.Should().Be(AuthenticationConstants.INVALID_TOKEN_MESSAGE);
        }

        [Test]
        public async Task TokenExpired()
        {
            LogTestCase();

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, AuthenticationTestConstants.EXPIRED_TOKEN);

            HttpResponseMessage response = await TestClient.GetAsync(EndpointConstants.API_V1_TEST);
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            string content = await response.Content.ReadAsStringAsync();

            ErrorResponse<CustomProblemDetails>? errorResponse = JsonConvert.DeserializeObject<ErrorResponse<CustomProblemDetails>>(content);

            CustomProblemDetails? customProblemDetails = errorResponse?.Error;
            Assert.IsNotNull(customProblemDetails);

            customProblemDetails?.Error.Code.Should().Be(ErrorCode.INVALID_TOKEN);
            customProblemDetails?.Error.Message.Should().Be(AuthenticationConstants.EXPIRED_TOKEN_MESSAGE);
        }

        [Test]
        public async Task TokenWithoutUniqueNameClaim()
        {
            LogTestCase();

            string bearerToken = AuthorizationUtils.GeneratePortalJwt(
                AuthenticationTestConstants.APPLICATION_SERVER_KEY,
                null,
                true
            );

            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthorizationTestConstants.BEARER, bearerToken);

            HttpResponseMessage response = await TestClient.GetAsync(EndpointConstants.API_V1_TEST);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            string content = await response.Content.ReadAsStringAsync();

            ErrorResponse<CustomProblemDetails>? errorResponse = JsonConvert.DeserializeObject<ErrorResponse<CustomProblemDetails>>(content);

            CustomProblemDetails? customProblemDetails = errorResponse?.Error;
            Assert.IsNotNull(customProblemDetails);

            customProblemDetails?.Error.Code.Should().Be(ErrorCode.INVALID_TOKEN);
            customProblemDetails?.Error.Message.Should().Be(AuthenticationConstants.IDENTITY_CLAIM_MISSING_MESSAGE);
        }
    }
}