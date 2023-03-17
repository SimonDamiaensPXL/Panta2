using AutoMapper;

namespace Panta2.Core.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile() 
        {
            CreateMap<Entities.Service, Models.ServiceDto>();
        }
    }
}
