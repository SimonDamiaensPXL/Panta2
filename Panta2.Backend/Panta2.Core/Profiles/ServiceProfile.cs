using AutoMapper;
using Panta2.Core.Models.Service;

namespace Panta2.Core.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile() 
        {
            CreateMap<Entities.Service, ServiceModel>();
            CreateMap<ServiceModel, Entities.Service>();
            CreateMap<ServiceCreationModel, Entities.Service>();
            CreateMap<Entities.Service, ServiceCreationModel>();
            CreateMap<ServiceNameUpdateModel, Entities.Service>();
            CreateMap<ServiceLinkUpdateModel, Entities.Service>();
            CreateMap<ServiceIconUpdateModel, Entities.Service>();
        }
    }
}
