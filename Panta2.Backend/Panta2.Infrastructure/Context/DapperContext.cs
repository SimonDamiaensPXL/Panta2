using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Panta2.Core.Entities;
using System.Data;

namespace Panta2.Infrastructure.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");

            SqlMapperExtensions.TableNameMapper = entityType =>
            {
                if (entityType == typeof(Company))
                {
                    return "companies";
                }
                else if (entityType == typeof(Service))
                {
                    return "services";
                }
                else if (entityType == typeof(User))
                {
                    return "AspNetUsers";
                }
                else if (entityType == typeof(Role))
                {
                    return "AspNetRoles";
                }
                else if (entityType == typeof(CompanyRole))
                {
                    return "CompanyRole";
                }
                else if (entityType == typeof(ServiceRole))
                {
                    return "ServiceRole";
                }
                throw new Exception($"Not supported entity type {entityType}");
            };
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
