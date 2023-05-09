using AutoMapper;
using Panta2.Core.Models.User;

namespace Panta2.Core.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<Entities.User, UserModel>();
            CreateMap<UserModel, Entities.User>();
            CreateMap<UserRegistrationModel, Entities.User>();
        }
    }
}
