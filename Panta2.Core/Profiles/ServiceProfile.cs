using AutoMapper;

namespace Panta2.Core.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile() 
        {
            CreateMap<Entities.Service, Models.ServiceDto>();
            CreateMap<Models.ServiceForCreationDto, Entities.Service>();
            CreateMap<Entities.Service, Models.ServiceForCreationDto>();
            CreateMap<Models.ServiceDto, Entities.Service>();
        }
    }
}
