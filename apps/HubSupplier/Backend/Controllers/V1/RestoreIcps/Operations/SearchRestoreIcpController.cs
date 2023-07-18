using Aseme.Apps.HubSupplier.Backend.Controllers.Contracts;
using Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Controller;
using Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Response;
using Aseme.Apps.HubSupplier.Backend.Extensions;
using Aseme.HubSupplier.RestoreIcps.Application.Search;
using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.Shared.Domain;
using Aseme.Shared.Infrastructure.Http.Response;
using Aseme.Shared.Infrastructure.Http.Response.Error;
using Aseme.Shared.Infrastructure.Services.PageUri;
using Aseme.Shared.Infrastructure.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Operations
{
    [ControllerName(ApiRoutes.RestoreIcps.Endpoint)]
    public class SearchRestoreIcpController : BaseV1RestoreIcpController
    {
        private readonly ISearchRestoreIcpService _searchRestoreIcpService;

        public SearchRestoreIcpController(ILogger<SearchRestoreIcpController> logger, IMapper mapper, ISearchRestoreIcpService searchRestoreIcpService, IUriService uriService) : base(mapper, logger, uriService)
        {
            _searchRestoreIcpService = searchRestoreIcpService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<RestoreIcpResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse<CustomProblemDetails>), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse<CustomProblemDetails>), (int)HttpStatusCode.ServiceUnavailable)]
        [SwaggerOperation(
           Summary = "Search restore ICP operations by filter",
           Description = "This endpoint will accept a filter and return a list of restore ICPs to the client.",
           Tags = new[] { ApiRoutes.RestoreIcps.Endpoint })]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PagedResponse<RestoreIcpResponse>))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, Type = typeof(ErrorResponse<CustomProblemDetails>))]
        public async Task<ActionResult<PagedResponse<RestoreIcpResponse>>> SearchAsync([FromQuery, SwaggerParameter("The filter parameter", Required = true)] RestoreIcpFilter filter)
        {
            _logger.LogInformation($"{nameof(SearchAsync)}, filter:{filter.ToJson()}.");
            PageResult<RestoreIcp> entityPage = await _searchRestoreIcpService.SearchAsync(filter);
            if (0 == entityPage.TotalPages) { return NoContent(); }
            PagedResponse<RestoreIcpResponse> responsePage = entityPage.ToPagedResponse<RestoreIcp, RestoreIcpResponse>(filter, _mapper, _uriService, Request.Path.Value, Request.Query);
            return Ok(responsePage);
        }
    }
}