using Dapper;
using Dapper.Contrib.Extensions;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Infrastructure.Context;

namespace Panta2.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<User> GetById(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.GetAsync<User>(id);
            }
        }

        public async Task<User> CreateAsync(User user)
        {
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.InsertAsync(user);
                user.Id = id;
                return user;
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            const string query = "SELECT * FROM AspNetUsers WHERE UserName = @UserName";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<User>(query, new { username });
            }
        }

        public async Task<bool> AddFavoriteService(int userId, int serviceId)
        {
            var query = "INSERT INTO Favorites (UserId, ServiceId) " +
                        "VALUES (@userId, @serviceId)";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { userId, serviceId });
                return rowsAffected == 1;
            }
        }

        public async Task<bool> RemoveFavoriteService(int userId, int serviceId)
        {
            var query = "DELETE FROM Favorites " +
                        "WHERE UserId = @userId AND ServiceId = @serviceId";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { userId, serviceId });
                return rowsAffected == 1;
            }
        }
    }
}
