using Dapper;
using Dapper.Contrib.Extensions;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models.Role;
using Panta2.Core.Models.User;
using Panta2.Infrastructure.Context;

namespace Panta2.Infrastructure
{
    public class CompanyRepository : ICompanyRepository
    {
        private DapperContext _context;

        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.GetAllAsync<Company>();
            }
        }

        public async Task<Company> GetById(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.GetAsync<Company>(id);
            }
        }

        public async Task<string> GetLogo(int id)
        {
            var query = "SELECT Logo FROM Companies c WHERE c.Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var logo = await connection.QueryFirstOrDefaultAsync<string>(query, new { id });
                return logo;
            }
        }

        public async Task<Company> Add(Company company)
        {
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.InsertAsync(company);
                company.Id = id;
                return company;
            }
        }

        public async Task<bool> UpdateName(Company company)
        {
            var query = "UPDATE Companies SET Name = @name WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { name = company.Name, id = company.Id });
                return rowsAffected == 1;
            }
        }

        public async Task<bool> UpdateLogo(Company company)
        {
            var query = "UPDATE Companies SET Logo = @logo WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { logo = company.Logo, id = company.Id });
                return rowsAffected == 1;
            }
        }

        public async Task<bool> Remove(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.DeleteAsync(new Company { Id = id });
            }
        }

        public async Task<IEnumerable<UserWithRoleNameModel>> GetUsersWithRoleNameFromCompany(int id)
        {
            var query = "SELECT u.Id, u.Firstname, u.Lastname, u.Email, r.Name AS RoleName " +
                        "FROM AspNetUsers u " +
                        "JOIN AspNetUserRoles ur ON u.Id = ur.UserId " +
                        "JOIN AspNetRoles r ON ur.RoleId = r.Id " +
                        "WHERE u.CompanyId = @id";
            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<UserWithRoleNameModel>(query, new { id });
                return users;
            }
        }

        public async Task<IEnumerable<RoleModel>> GetRolesFromCompany(int id)
        {
            var query = "SELECT r.Id, r.Name " +
                        "FROM AspNetRoles r " +
                        "JOIN CompanyRole cr ON r.Id = cr.RoleId " +
                        "WHERE cr.CompanyId = @id";
            using (var connection = _context.CreateConnection())
            {
                var roles = await connection.QueryAsync<RoleModel>(query, new { id });
                return roles;
            }
        }
    }
}
