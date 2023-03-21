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
    }
}
