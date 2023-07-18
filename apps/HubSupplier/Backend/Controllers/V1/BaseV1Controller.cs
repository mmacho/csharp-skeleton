using Aseme.Apps.HubSupplier.Backend.Controllers.Contracts;
using Aseme.Shared.Infrastructure.Services.PageUri;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Aseme.Apps.HubSupplier.Backend.Controllers.V1
{
    [ApiVersion(ApiRoutes.ApiVersion_V1_0)]
    public class BaseV1Controller : BaseController
    {
        public BaseV1Controller(IMapper mapper, ILogger logger, IUriService uriService) : base(mapper, logger, uriService)
        {
        }
    }
}