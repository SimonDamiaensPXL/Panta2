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
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                return await connection.QueryFirstOrDefaultAsync<string>(query, new { id });
            }
        }

        public async Task<Role> GetRoleById(int id)
        {
            var query = "SELECT Id, Name FROM AspNetRoles WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Role>(query, new { id });
            }
        }

        public async Task<IEnumerable<Service>> GetServicesFromRole(int id)
        {
            var query = "SELECT s.Id, s.Name " +
                        "FROM AspNetRoles r " +
                        "INNER JOIN ServiceRole sr ON r.Id = sr.RoleId " +
                        "INNER JOIN Services s ON sr.ServiceId = s.Id " +
                        "WHERE r.Id = @id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Service>(query, new { id });
            }
        }

        public async Task<IEnumerable<ServiceWithIsInRoleModel>> GetServicesFromCompanyWithIsInRole(int companyId, int roleId)
        {
            var query = "SELECT cs.ServiceId AS Id, cs.Name, sr.RoleId, " +
                        "       CASE WHEN sr.ServiceId IS NOT NULL THEN 1 ELSE 0 END AS IsInRole " +
                        "FROM CompanyService cs " +
                        "LEFT JOIN ServiceRole sr ON cs.ServiceId = sr.ServiceId AND sr.RoleId = @roleId " +
                        "WHERE cs.CompanyId = @companyId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<ServiceWithIsInRoleModel>(query, new { roleId, companyId });
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

        public async Task<IEnumerable<UserWithRoleNameModel>> GetUsersWithRoleNameFromCompany(int id)
        {
            var query = "SELECT u.Id, u.Firstname, u.Lastname, u.Email, r.Name AS RoleName " +
                        "FROM AspNetUsers u " +
                        "JOIN AspNetUserRoles ur ON u.Id = ur.UserId " +
                        "JOIN AspNetRoles r ON ur.RoleId = r.Id " +
                        "WHERE u.CompanyId = @id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<UserWithRoleNameModel>(query, new { id });
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
                return await connection.QueryAsync<RoleModel>(query, new { id });
            }
        }

        public async Task<int> AddNewRoleToCompany(RoleCreationModel model, int companyId)
        {
            Role newRole = new()
            {
                Name = model.Name,
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

                var serviceRoles = model.Services.Select(e => new ServiceRole { RoleId =  roleId, ServiceId = e }).ToList();

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
                return await connection.QueryAsync<ServiceModel>(query, new { id });
            }
        }

        public async Task<ServiceModel> GetServiceFromCompany(int companyId, int serviceId)
        {
            var query = "SELECT ServiceId AS Id, Name, Icon " +
                        "FROM CompanyService " +
                        "WHERE CompanyId = @companyId AND ServiceId = @serviceId";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<ServiceModel>(query, new { companyId, serviceId });
            }
        }

        public async Task<IEnumerable<ServiceNameModel>> GetServiceNamesFromCompany(int id)
        {
            var query = "SELECT ServiceId AS Id,Name " +
                        "FROM CompanyService " +
                        "WHERE CompanyId = @id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<ServiceNameModel>(query, new { id });
            }
        }

        public async Task<IEnumerable<ServiceNameModel>> GetServiceNamesNotInCompany(int id)
        {
            var query = "SELECT Id, Name " +
                        "FROM Services " +
                        "WHERE Id NOT IN (" +
                        "    SELECT ServiceId " +
                        "    FROM CompanyService " +
                        "    WHERE CompanyId = @id " +
                        ")";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<ServiceNameModel>(query, new { id });
            }
        }

        public async Task<int> AddServicesToCompany(CompanyServicesCreationModel model)
        {
            var query = "SELECT Id, Name, Icon " +
                        "FROM Services " +
                        "WHERE Id NOT IN (" +
                        "    SELECT ServiceId " +
                        "    FROM CompanyService " +
                        "    WHERE CompanyId = @id " +
                        ")";

            using (var connection = _context.CreateConnection())
            {
                var services = await connection.QueryAsync<Service>(query, new { id = model.companyId });

                var companyServices = model.Services.Select(e => new CompanyService
                {
                    CompanyId = model.companyId,
                    ServiceId = e,
                    Name = services.FirstOrDefault(s => s.Id == e)?.Name,
                    Icon = services.FirstOrDefault(s => s.Id == e)?.Icon,
                    Enabled = true,
                    Order = 0
                }).ToList();

                await connection.InsertAsync(companyServices);

            }

            return model.companyId;
        }

        public async Task<bool> UpdateServiceName(Service service, int companyId)
        {
            var query = "UPDATE CompanyService SET Name = @name WHERE CompanyId = @companyId AND ServiceId = @serviceId";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { name = service.Name, companyId, serviceId = service.Id });
                return rowsAffected == 1;
            }
        }

        public async Task<bool> UpdateServiceIcon(Service service, int companyId)
        {
            var query = "UPDATE CompanyService SET Icon = @icon WHERE CompanyId = @companyId AND ServiceId = @serviceId";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { icon = service.Icon, companyId, serviceId = service.Id });
                return rowsAffected == 1;
            }
        }

        public async Task<bool> UpdateRoleName(int roleId, string name)
        {
            var query = "UPDATE AspNetRoles SET Name = @name WHERE Id = @roleId";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { roleId, name });
                return rowsAffected == 1;
            }
        }

        public async Task<bool> UpdateRoleServices(int roleId, int[] serviceIds)
        {
            var query = "DELETE FROM ServiceRole WHERE RoleId = @roleId";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { roleId });

                var serviceRoles = serviceIds.Select(e => new ServiceRole { RoleId = roleId, ServiceId = e }).ToList();

                await connection.InsertAsync(serviceRoles);

                return rowsAffected != 0;
            }
        }

        public async Task<bool> Remove(int id)
        {
            var query = "SELECT RoleId FROM CompanyRole WHERE CompanyId = @id";

            using (var connection = _context.CreateConnection())
            {
                var roleIds = await connection.QueryAsync<int>(query, new { id });

                query = "DELETE FROM AspNetUserRoles " +
                        "WHERE UserId IN ( " +
                        "    SELECT Id " +
                        "    FROM AspNetUsers " +
                        "    WHERE CompanyId = @id " +
                        ")";
                var rowsAffected = await connection.ExecuteAsync(query, new { id });

                query = "DELETE FROM Favorites " +
                        "WHERE UserId IN ( " +
                        "    SELECT Id " +
                        "    FROM AspNetUsers " +
                        "    WHERE CompanyId = @id " +
                        ")";
                rowsAffected = await connection.ExecuteAsync(query, new { id });

                query = "DELETE FROM CompanyRole WHERE CompanyId = @id";
                rowsAffected = await connection.ExecuteAsync(query, new { id });

                query = "DELETE FROM ServiceRole WHERE RoleId IN @roleIds";
                rowsAffected = await connection.ExecuteAsync(query, new { roleIds });

                query = "DELETE FROM AspNetRoles WHERE Id IN @roleIds";
                rowsAffected = await connection.ExecuteAsync(query, new { roleIds });

                query = "DELETE FROM AspNetUsers WHERE CompanyId = @id";
                rowsAffected = await connection.ExecuteAsync(query, new { id });

                query = "DELETE FROM CompanyService WHERE CompanyId = @id";
                rowsAffected = await connection.ExecuteAsync(query, new { id });

                return await connection.DeleteAsync(new Company { Id = id });
            }
        }

        public async Task<bool> RemoveCompanyService(int companyId, int serviceId)
        {
            var query = "SELECT * FROM ServiceRole sr " +
                        "INNER JOIN CompanyRole cr ON @companyId = cr.CompanyId " +
                        "WHERE sr.ServiceId = @serviceId AND cr.RoleId = sr.RoleId";

            using (var connection = _context.CreateConnection())
            {
                var checkRow = await connection.QueryFirstOrDefaultAsync(query, new { companyId, serviceId });
                if (checkRow != null)
                {
                    throw new InvalidOperationException();
                }

                query = "DELETE FROM CompanyService WHERE CompanyId = @companyId AND ServiceId = @serviceId";
                var rowsAffected = await connection.ExecuteAsync(query, new { companyId, serviceId });


                return rowsAffected == 1;
            }
        }

        public async Task<bool> RemoveRole(int roleId)
        {
            var query = "SELECT * FROM AspNetUserRoles WHERE RoleId = @roleId";

            using (var connection = _context.CreateConnection())
            {
                var checkRow = await connection.QueryFirstOrDefaultAsync(query, new { roleId });
                if (checkRow != null)
                {
                    throw new InvalidOperationException();
                }

                query = "DELETE FROM ServiceRole WHERE RoleId = @roleId";
                var rowsAffected = await connection.ExecuteAsync(query, new { roleId });
                if (rowsAffected == 0)
                {
                    return false;
                }

                query = "DELETE FROM CompanyRole WHERE RoleId = @roleId";
                rowsAffected = await connection.ExecuteAsync(query, new { roleId });

                return rowsAffected == 1;
            }
        }

    }
}
