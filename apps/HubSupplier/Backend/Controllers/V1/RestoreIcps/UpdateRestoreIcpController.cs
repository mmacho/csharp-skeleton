using Aseme.Apps.HubSupplier.Backend.Controllers.Contracts;
using Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Request;
using Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Response;
using Aseme.HubSupplier.RestoreIcps.Application.Update;
using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.Shared.Infrastructure.Http.Response;
using Aseme.Shared.Infrastructure.Http.Response.Error;
using Aseme.Shared.Infrastructure.Services.PageUri;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps
{
    [ControllerName(ApiRoutes.RestoreIcps.Endpoint)]
    public class UpdateRestoreIcpController : BaseV1RestoreIcpController
    {
        private readonly IUpdateRestoreIcpService _updateRestoreIcpService;

        public UpdateRestoreIcpController(ILogger<UpdateRestoreIcpController> logger, IMapper mapper, IUpdateRestoreIcpService updateRestoreIcpService, IUriService uriService) : base(mapper, logger, uriService)
        {
            _updateRestoreIcpService = updateRestoreIcpService;
        }

        [HttpPut(ApiRoutes.Id)]
        [ProducesResponseType(typeof(Response<RestoreIcpResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [ProducesResponseType((int)HttpStatusCode.Conflict, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [SwaggerOperation(
           Summary = "Update a restore ICP operation by identifier",
           Description = "This endpoint will take in an update operation and return it to the client.",
           Tags = new[] { ApiRoutes.RestoreIcps.Endpoint })]
        [SwaggerResponse(StatusCodes.Status200OK, "The posted restore ICP payload response", Type = typeof(Response<RestoreIcpResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        public async Task<ActionResult<Response<RestoreIcpResponse>>> UpdateAsync([FromRoute, SwaggerParameter("The restore ICP identifier", Required = true)] long id, [FromBody, SwaggerRequestBody("The restore ICP request payload", Required = true)] UpdateRestoreIcpRequest request)
        {
            request.Id = id;
            RestoreIcp entity = await _updateRestoreIcpService.UpdateAsync(id, _mapper.Map<RestoreIcp>(request));
            RestoreIcpResponse response = _mapper.Map<RestoreIcpResponse>(entity);
            return Ok(Response<RestoreIcpResponse>.Success(response));
        }
    }
}