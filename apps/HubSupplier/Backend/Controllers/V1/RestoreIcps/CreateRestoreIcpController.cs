using Aseme.Apps.HubSupplier.Backend.Controllers.Contracts;
using Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Request;
using Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Response;
using Aseme.HubSupplier.RestoreIcps.Application.Create;
using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.Shared.Infrastructure.Http.Response;
using Aseme.Shared.Infrastructure.Http.Response.Error;
using Aseme.Shared.Infrastructure.Services.PageUri;
using Aseme.Shared.Infrastructure.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps
{
    [Authorize(Policy = AuthorizationConstants.PORTAL_POLICY)]
    [ControllerName(ApiRoutes.RestoreIcps.Endpoint)]
    public class CreateRestoreIcpController : BaseV1RestoreIcpController
    {
        private readonly ICreateRestoreIcpService _createRestoreIcpService;

        public CreateRestoreIcpController(ILogger<CreateRestoreIcpController> logger, IMapper mapper, ICreateRestoreIcpService createRestoreIcpService, IUriService uriService) : base(mapper, logger, uriService)
        {
            _createRestoreIcpService = createRestoreIcpService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<RestoreIcpResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [SwaggerOperation(
           Summary = "Add a new restore ICP operation",
           Description = "This endpoint will take in a create operation and return it to the client.",
           Tags = new[] { ApiRoutes.RestoreIcps.Endpoint })]
        [SwaggerResponse(StatusCodes.Status201Created, "The posted restore ICP payload response", Type = typeof(Response<RestoreIcpResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        public async Task<ActionResult<Response<RestoreIcpResponse>>> CreateAsync([FromBody, SwaggerRequestBody("The restore ICP request payload", Required = true)] CreateRestoreIcpRequest request)
        {
            _logger.LogInformation($"{nameof(CreateAsync)}, request:{request.ToJson()}.");
            RestoreIcp entity = await _createRestoreIcpService.CreateAsync(_mapper.Map<RestoreIcp>(request));
            RestoreIcpResponse response = _mapper.Map<RestoreIcpResponse>(entity);
            return CreatedAtAction(nameof(GetRestoreIcpController.GetAsync), ApiRoutes.RestoreIcps.Endpoint, new { id = response.Id }, Response<RestoreIcpResponse>.Success(response));
        }
    }
}