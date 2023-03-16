using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Infrastructure.Context;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Dapper;

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
            var query = "SELECT * FROM Companies";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }

        public Company GetById(int id)
        {
            throw new NotImplementedException();
        }
        public Company Add(Company service)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Company Update(Company service)
        {
            throw new NotImplementedException();
        }
    }
}
