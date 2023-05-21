using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.IdentityModel.Tokens;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models.Company;
using Panta2.Core.Models.Role;
using Panta2.Core.Models.Service;
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

        public async Task<Role> AddNewRoleToCompany(RoleCreationModel model, int companyId)
        {
            Role newRole = new()
            {
                Name = model.Name
                Name = model.Name,
                NormalizedName = model.Name.ToUpper(),
                Discriminator = "ApplicationRole"
            };

            var query = "SELECT r.Name " +
                        "FROM AspNetRoles r " +
                        "JOIN CompanyRole cr ON r.Id = cr.RoleId " +
                        "WHERE r.Name = @name AND cr.CompanyId = @id";

            using (var connection = _context.CreateConnection())
            {
                var role = await connection.QueryAsync<RoleModel>(query, new { name = model.Name, id = companyId });

                if (!role.IsNullOrEmpty())
                {
                    return 0;
                }

                var roleId = await connection.InsertAsync(newRole);

                CompanyRole newCompanyRole = new()
                {
                    CompanyId = companyId,
                    RoleId = roleId
                };

                await connection.InsertAsync(newCompanyRole);

                var serviceRoles = model.Services.Select(e => new RoleService { RoleId =  roleId, ServiceId = e }).ToList();

                await connection.InsertAsync(serviceRoles);

            }

            return companyId;
        }

        public async Task<IEnumerable<ServiceModel>> GetServicesFromCompany(int id)
        {
            var query = "SELECT cs.ServiceId AS Id, cs.Name, cs.Icon, s.Link " +
                        "FROM CompanyService cs " +
                        "JOIN Services s ON cs.ServiceId = s.Id " +
                        "WHERE CompanyId = @id";
            using (var connection = _context.CreateConnection())
            {
                var companyServices = await connection.QueryAsync<ServiceModel>(query, new { id });
                return companyServices;
            }
        }

        public async Task<IEnumerable<ServiceNameModel>> GetServiceNamesFromCompany(int id)
        {
            var query = "SELECT ServiceId AS Id,Name " +
                        "FROM CompanyService " +
                        "WHERE CompanyId = @id";

            using (var connection = _context.CreateConnection())
            {
                var companyServices = await connection.QueryAsync<ServiceNameModel>(query, new { id });
                return companyServices;
            }
        }
    }
}
