using Dapper;
using Dapper.Contrib.Extensions;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models.Service;
using Panta2.Core.Models.User;
using Panta2.Infrastructure.Context;
using System.Data;

namespace Panta2.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.GetAllAsync<User>();
            }
        }

        public async Task<User> GetById(int id)
        {
            var query = "SELECT Id, UserName, Firstname, Lastname, Email, CompanyId " +
                        "FROM AspNetUsers " +
                        "WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<User>(query, new { id });
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            const string query = "SELECT * FROM AspNetUsers WHERE UserName = @username";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<User>(query, new { username });
            }
        }

        public async Task<User> CreateAsync(User user, int roleId)
        {
            var query = "INSERT INTO AspNetUserRoles (UserId, RoleId) " +
                                 "VALUES (@userId, @roleId)";

            var checkUser = await GetUserByUsername(user.UserName);

            if (checkUser != null)
            {
                throw new ConstraintException();
            }

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.InsertAsync(user);
                user.Id = id;

                await connection.ExecuteAsync(query, new { userId = id, roleId });

                return user;
            }
        }

        public async Task<bool> UpdateFirstName(string firstname, int id)
        {
            const string query = "UPDATE AspNetUsers SET FirstName = @firstname WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { firstname, id });
                return rowsAffected == 1;
            }
        }

        public async Task<bool> UpdateUser(UserUserNameUpdateModel model)
        {
            const string query = "UPDATE AspNetUsers SET UserName = @username WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { username = model.UserName, model.Id });
                return rowsAffected == 1;
            }
        }

        public async Task<bool> UpdateUser(UserNameUpdateModel model)
        {
            const string query = "UPDATE AspNetUsers " +
                                 "SET FirstName = @firstname, LastName = @lastname " +
                                 "WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { firstname = model.FirstName, lastname = model.LastName, id = model.Id });
                return rowsAffected == 1;
            }
        }

        public async Task<bool> UpdateUser(UserEmailUpdateModel model)
        {
            const string query = "UPDATE AspNetUsers SET Email = @email WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { email = model.Email, id = model.Id });
                return rowsAffected == 1;
            }
        }

        public async Task<bool> UpdateUser(UserPasswordUpdateModel model)
        {
            const string query = "UPDATE AspNetUsers SET PasswordHash = @password WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { password = model.Password, id = model.Id });
                return rowsAffected == 1;
            }
        }

        public async Task<bool> UpdateUser(UserRoleUpdateModel model)
        {
            const string query = "UPDATE AspNetUserRoles SET RoleId = @newRoleId WHERE UserId = @userId AND RoleId = @roleId";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { newRoleId = model.NewRoleId, userId = model.Id, roleId = model.RoleId });
                return rowsAffected == 1;
            }
        }

        public async Task<Role> GetRoleFromUser(int id)
        {
            var query = "SELECT r.Id, r.Name " +
                        "FROM AspNetUserRoles ur " +
                        "INNER JOIN AspNetRoles r ON ur.RoleId = r.Id " +
                        "WHERE ur.UserId = @id";

            using (var connection = _context.CreateConnection())
            {
                var role = await connection.QueryFirstOrDefaultAsync<Role>(query, new { id });
                return role;
            }
        }

        public async Task<IEnumerable<Service>> GetServicesFromUser(int id)
        {
            var query = "SELECT cs.ServiceId AS Id, cs.Name, cs.Icon, s.Link " +
                        "FROM AspNetUsers u " +
                        "INNER JOIN AspNetUserRoles ur ON u.Id = ur.UserId " +
                        "INNER JOIN AspNetRoles r ON ur.RoleId = r.Id " +
                        "INNER JOIN ServiceRole sr ON r.Id = sr.RoleId " +
                        "INNER JOIN CompanyService cs ON sr.ServiceId = cs.ServiceId AND u.CompanyId = cs.CompanyId " +
                        "INNER JOIN Services s ON cs.ServiceId = s.Id " +
                        "WHERE u.Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var services = await connection.QueryAsync<Service>(query, new { id });
                return services;
            }
        }

        public async Task<IEnumerable<Service>> GetFavoriteServicesFromUser(int id)
        {
            var query = "SELECT cs.ServiceId AS Id, cs.Name, cs.Icon, s.Link " +
                        "FROM CompanyService cs " +
                        "INNER JOIN Favorites f ON cs.ServiceId = f.ServiceId " +
                        "INNER JOIN Services s ON cs.ServiceId = s.Id " +
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
                        "INNER JOIN ServiceRole sr ON cs.ServiceId = sr.ServiceId " +
                        "INNER JOIN AspNetRoles r ON sr.RoleId = r.Id " +
                        "INNER JOIN AspNetUserRoles ur ON r.Id = ur.RoleId " +
                        "LEFT JOIN Favorites f ON cs.ServiceId = f.ServiceId AND f.UserId = @id " +
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

        public async Task<bool> RemoveUser(int userId)
        {
            var query = "DELETE FROM AspNetUserRoles " +
                        "WHERE UserId = @userId";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { userId });

                User user = new User { Id = userId };

                var isDeleted = await connection.DeleteAsync(user);

                return rowsAffected == 1 && isDeleted;
            }
        }
    }
}
