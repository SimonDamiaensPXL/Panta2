using Dapper;
using Dapper.Contrib.Extensions;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models;
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
        public async Task<IEnumerable<Service>> GetServicesFromUser(int id)
        {
            var query = "SELECT cs.* " +
                        "FROM AspNetUsers u " +
                        "INNER JOIN AspNetUserRoles ur ON u.Id = ur.UserId " +
                        "INNER JOIN AspNetRoles r ON ur.RoleId = r.Id " +
                        "INNER JOIN RoleService rs ON r.Id = rs.RoleId " +
                        "INNER JOIN CompanyService cs ON rs.ServiceId = cs.ServiceId " +
                        "WHERE u.Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var services = await connection.QueryAsync<Service>(query, new { id });
                return services;
            }
        }

        public async Task<IEnumerable<Service>> GetFavoriteServicesFromUser(int id)
        {
            var query = "SELECT cs.CompanyId, cs.ServiceId, cs.Name, cs.Icon, cs.Enabled, cs.[Order] " +
                        "FROM CompanyService cs " +
                        "INNER JOIN Favorites f ON cs.ServiceId = f.ServiceId " +
                        "WHERE f.UserId = @id";

            using (var connection = _context.CreateConnection())
            {
                var services = await connection.QueryAsync<Service>(query, new { id });
                return services;
            }
        }

        public async Task<IEnumerable<SerivceWithIsFavoriteModel>> GetServicesWithIsFavorite(int id)
        {
            var query = "SELECT cs.CompanyId, cs.ServiceId, cs.Name, cs.Icon, cs.Enabled, " +
                        "CASE WHEN f.ServiceId IS NULL THEN 0 ELSE 1 END AS isFavorite " +
                        "FROM CompanyService cs " +
                        "INNER JOIN RoleService rs ON cs.ServiceId = rs.ServiceId " +
                        "INNER JOIN AspNetRoles r ON rs.RoleId = r.Id " +
                        "INNER JOIN AspNetUserRoles ur ON r.Id = ur.RoleId " +
                        "LEFT JOIN Favorites f ON cs.ServiceId = f.ServiceId AND f.UserId = 4 " +
                        "WHERE ur.UserId = @id";

            using (var connection = _context.CreateConnection())
            {
                var services = await connection.QueryAsync<SerivceWithIsFavoriteModel>(query, new { id });
                return services;
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
