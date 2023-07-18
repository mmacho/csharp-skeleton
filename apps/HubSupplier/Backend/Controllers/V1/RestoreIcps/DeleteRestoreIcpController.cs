using Aseme.Apps.HubSupplier.Backend.Controllers.Contracts;
using Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Controller;
using Aseme.HubSupplier.RestoreIcps.Application.Delete;
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
    public class DeleteRestoreIcpController : BaseV1RestoreIcpController
    {
        private readonly IDeleteRestoreIcpService _deleteRestoreIcpService;

        public DeleteRestoreIcpController(ILogger<DeleteRestoreIcpController> logger, IMapper mapper, IDeleteRestoreIcpService deleteRestoreIcpService, IUriService uriService) : base(mapper, logger, uriService)
        {
            _deleteRestoreIcpService = deleteRestoreIcpService;
        }

        [HttpDelete(ApiRoutes.Id)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [SwaggerOperation(
           Summary = "Delete a restore ICP operation by identifier",
           Description = "This endpoint will take in a delete operation and return it to the client.",
           Tags = new[] { ApiRoutes.RestoreIcps.Endpoint })]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        public async Task<ActionResult> DeleteAsync([FromRoute, SwaggerParameter("The restore ICP identifier", Required = true)] long id)
        {
            await _deleteRestoreIcpService.DeleteAsync(id);
            return NoContent();
        }
    }
}