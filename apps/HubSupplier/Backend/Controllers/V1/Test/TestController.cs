using Aseme.Shared.Infrastructure.Services.PageUri;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Aseme.Apps.HubSupplier.Backend.Controllers.V1.Test
{
    [ApiVersion("1")]
    public class TestController : BaseController
    {
        public TestController(IMapper mapper, ILogger<TestController> logger, IUriService uriService) : base(mapper, logger, uriService)
        { }

        [HttpGet()]
        public ObjectResult Get()
        {
            return Ok("data");
        }
    }
}