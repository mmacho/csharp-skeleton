using Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Request;
using Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Models.Response;
using Aseme.HubSupplier.RestoreIcps.Domain;
using Aseme.Shared.Domain;
using Aseme.Shared.Infrastructure.Http.Response;
using AutoMapper;

namespace Aseme.Apps.HubSupplier.Backend.Controllers.V1.RestoreIcps.Profiles
{
    public class RestoreIcpMappingProfile : Profile
    {
        public RestoreIcpMappingProfile()
        {
            //TODO: GENERIC!
            CreateMap<PageResult<RestoreIcp>, PagedResponse<RestoreIcpResponse>>().ConstructUsing((source, context) => PagedResponse<RestoreIcpResponse>.Success(
                context.Mapper.Map<List<RestoreIcp>, List<RestoreIcpResponse>>(source.Data),
                source.TotalCount,
                source.PageNumber,
                source.PageSize));

            CreateMap<CreateRestoreIcpRequest, RestoreIcp>();

            CreateMap<UpdateRestoreIcpRequest, RestoreIcp>();
            CreateMap<UpdateRestoreIcpDetailsRequest, RestoreIcpDetails>();

            //.ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.FromBase64String(src.Version)));

            CreateMap<RestoreIcp, RestoreIcpResponse>();
            CreateMap<RestoreIcpDetails, RestoreIcpDetailsResponse>();
            //.ForMember(dest => dest.Version, opt => opt.MapFrom(src => Convert.ToBase64String(src.Version)));

            CreateMap<RestoreIcpDetails, RestoreIcpDetails>()
                .ForMember(dest => dest.RestoreIcpId, o => o.Ignore());

            // para que no mapee el id, fecha de creación, update, etc.
            CreateMap<RestoreIcp, RestoreIcp>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(dest => dest.OwnerId, o => o.Ignore())
                .ForMember(dest => dest.CreatedDate, o => o.Ignore())
                .ForMember(dest => dest.LastModifiedDate, o => o.Ignore());
        }
    }
}