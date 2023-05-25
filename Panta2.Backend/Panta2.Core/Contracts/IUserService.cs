using Panta2.Core.Entities;
using Panta2.Core.Models.Role;
using Panta2.Core.Models.Service;
using Panta2.Core.Models.User;

namespace Panta2.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetUserList();
        Task<User> GetUserById(int id);
        Task<UserModel> RegisterUser(UserRegistrationModel user);
        Task<UserModel> LoginUser(string username, string password);
        Task<bool> ChangeFirstName(string username, int id);
        Task<bool> ChangeUser(UserUserNameUpdateModel model);
        Task<bool> ChangeUser(UserNameUpdateModel model);
        Task<bool> ChangeUser(UserEmailUpdateModel model);
        Task<bool> ChangeUser(UserPasswordUpdateModel model);
        Task<bool> ChangeUser(UserRoleUpdateModel model);
        Task<RoleModel> GetRoleFromUser(int id);
        Task<IEnumerable<ServiceModel>> GetAllServicesFromUser(int id);
        Task<IEnumerable<ServiceModel>> GetAllFavoriteServicesFromUser(int id);
        Task<IEnumerable<SerivceWithIsFavoriteModel>> GetAllServicesWithIsFavoriteFromUser(int id);
        Task<bool> EditFavoritesFromUser(int userId, int serviceId, bool isFavorite);
        Task<bool> DeleteUser(int id);
    }
}
