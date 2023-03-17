using Dapper.Contrib.Extensions;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Infrastructure.Context;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        public async Task<Company> Add(Company company)
        {
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.InsertAsync(company);
                company.Id = id;
                return company;
            }
        }

        public async Task<bool> Update(Company company)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.UpdateAsync(company);
            }
        }

        public async Task<bool> Remove(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.DeleteAsync(new Company { Id = id });
            }
        }
    }
}
