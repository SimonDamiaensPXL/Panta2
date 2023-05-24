using Panta2.Core.Entities;
using Panta2.Core.Models.Service;
using Panta2.Core.Models.User;

namespace Panta2.Core.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> CreateAsync(User user, int roleId);
        Task<bool> UpdateFirstName(string username, int id);
        Task<bool> UpdateUser(UserUserNameUpdateModel model);
        Task<bool> UpdateUser(UserNameUpdateModel model);
        Task<bool> UpdateUser(UserEmailUpdateModel model);
        Task<bool> UpdateUser(UserPasswordUpdateModel model);
        Task<User> GetUserByUsername(string username);
        Task<IEnumerable<Service>> GetServicesFromUser(int id);
        Task<IEnumerable<Service>> GetFavoriteServicesFromUser(int id);
        Task<IEnumerable<SerivceWithIsFavoriteModel>> GetServicesWithIsFavorite(int id);
        Task<bool> AddFavoriteService(int userId, int serviceId);
        Task<bool> RemoveFavoriteService(int userId, int serviceId);
    }
}
