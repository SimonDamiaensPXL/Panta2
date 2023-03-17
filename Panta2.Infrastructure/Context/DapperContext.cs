using Dapper.Contrib.Extensions;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper.Mapper;
using Panta2.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Panta2.Infrastructure.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

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
                throw new Exception($"Not supported entity type {entityType}");
            };
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
