using Microsoft.AspNetCore.Identity;
using Panta2.Core.Entities;
using Panta2.Core.Models;

namespace Panta2.Core.Contracts
{
    public interface IUserService
    {
        Task<UserModel> GetUserById(int id);
        Task<UserModel> RegisterUser(UserRegistrationModel user);
        Task<UserModel> LoginUser(string username, string password);
    }
}
