using Aseme.Shared.Infrastructure.Services.PageUri;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Aseme.Apps.HubSupplier.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    // TODO: ADD XML SUPPORT
    //[Consumes(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    //[Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    public abstract class BaseController : ControllerBase
    {
        public IMapper _mapper { get; }

        public ILogger _logger { get; }

        public IUriService _uriService { get; }

        protected BaseController(IMapper mapper, ILogger logger, IUriService uriService)
        {
            _mapper = mapper;
            _logger = logger;
            _uriService = uriService;
        }
    }
}
