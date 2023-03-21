using AutoMapper;

namespace Panta2.Core.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<Entities.User, Models.UserModel>();
            CreateMap<Models.UserModel, Entities.User>();
            CreateMap<Models.UserRegistrationModel, Entities.User>();
        }
    }
}
