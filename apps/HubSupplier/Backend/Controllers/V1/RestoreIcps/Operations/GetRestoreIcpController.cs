using Aseme.Apps.HubSupplier.Backend.Controllers.Contracts;
using Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Controller;
using Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Response;
using Aseme.HubSupplier.RestoreIcps.Application.Get;
using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.Shared.Infrastructure.Http.Response;
using Aseme.Shared.Infrastructure.Http.Response.Error;
using Aseme.Shared.Infrastructure.Services.PageUri;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Operations
{
    [ControllerName(ApiRoutes.RestoreIcps.Endpoint)]
    public class GetRestoreIcpController : BaseV1RestoreIcpController
    {
        private readonly IGetRestoreIcpService _getRestoreIcpService;

        public GetRestoreIcpController(ILogger<GetRestoreIcpController> logger, IMapper mapper, IGetRestoreIcpService getRestoreIcpService, IUriService uriService) : base(mapper, logger, uriService)
        {
            _getRestoreIcpService = getRestoreIcpService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Id)]
        [ProducesResponseType(typeof(Response<RestoreIcpResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [SwaggerOperation(
           Summary = "Get a restore ICP operation by identifier",
           Description = "This endpoint will accept an identifier and return a restore ICP to the client.",
           Tags = new[] { ApiRoutes.RestoreIcps.Endpoint })]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<RestoreIcpResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        public async Task<ActionResult<Response<RestoreIcpResponse>>> GetAsync([FromRoute, SwaggerParameter("The restore ICP identifier", Required = true)] long id)
        {
            _logger.LogInformation($"Fetch RestoreIcp with ID: {id} from the database");
            RestoreIcp entity = await _getRestoreIcpService.GetAsync(id);
            RestoreIcpResponse response = _mapper.Map<RestoreIcpResponse>(entity);
            _logger.LogInformation($"Returning RestoreIcp with ID: {response.Id}.");
            return Ok(Response<RestoreIcpResponse>.Success(response));
        }
    }
}