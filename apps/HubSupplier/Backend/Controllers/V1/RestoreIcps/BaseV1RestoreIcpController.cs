using Aseme.Apps.HubSupplier.Backend.Controllers.Contracts;
using Aseme.Shared.Infrastructure.Services.PageUri;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps
{
    [Route(ApiRoutes.BasePath + ApiRoutes.RestoreIcps.Endpoint)]
    [Route(ApiRoutes.BasePath + ApiRoutes.ApiVersion + ApiRoutes.RestoreIcps.Endpoint)]
    public class BaseV1RestoreIcpController : BaseV1Controller
    {
        public BaseV1RestoreIcpController(IMapper mapper, ILogger logger, IUriService uriService) : base(mapper, logger, uriService)
        {
        }
    }
}