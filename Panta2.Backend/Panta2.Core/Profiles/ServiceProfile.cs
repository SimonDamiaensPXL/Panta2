using AutoMapper;

namespace Panta2.Core.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile() 
        {
            CreateMap<Entities.Service, Models.ServiceModel>();
            CreateMap<Models.ServiceCreationModel, Entities.Service>();
            CreateMap<Entities.Service, Models.ServiceCreationModel>();
            CreateMap<Models.ServiceModel, Entities.Service>();
        }
    }
}
