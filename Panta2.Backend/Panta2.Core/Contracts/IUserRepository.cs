using Panta2.Core.Entities;

namespace Panta2.Core.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<User> CreateAsync(User user);
        Task<User> GetUserByUsername(string username);
        Task<bool> AddFavoriteService(int userId, int serviceId);
        Task<bool> RemoveFavoriteService(int userId, int serviceId);
    }
}
