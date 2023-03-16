using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using System.Data;

namespace Panta2.Infrastructure
{
    public class CompanyRepository : ICompanyRepository
    {
        private IDbConnection _db;

        public CompanyRepository(string connectionString)
        {
            _db = new SqlConnection(connectionString);
        }

        public List<Company> GetAll()
        {
            return _db.GetAll<Company>().ToList();
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
