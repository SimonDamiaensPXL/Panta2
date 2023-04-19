using Panta2.Core.Entities;
using Panta2.Core.Models;

namespace Panta2.Core.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> CreateAsync(User user);
        Task<bool> UpdateFirstName(string username, int id);
        Task<User> GetUserByUsername(string username);
        Task<IEnumerable<Service>> GetServicesFromUser(int id);
        Task<IEnumerable<Service>> GetFavoriteServicesFromUser(int id);
        Task<IEnumerable<SerivceWithIsFavoriteModel>> GetServicesWithIsFavorite(int id);
        Task<bool> AddFavoriteService(int userId, int serviceId);
        Task<bool> RemoveFavoriteService(int userId, int serviceId);
    }
}
