using AutoMapper;
using Panta2.Core.Entities;
using Panta2.Core.Models.Role;
using Panta2.Core.Models.User;

namespace Panta2.Core.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<UserRegistrationModel, User>();
            CreateMap<Role, RoleModel>();
        }
    }
}
