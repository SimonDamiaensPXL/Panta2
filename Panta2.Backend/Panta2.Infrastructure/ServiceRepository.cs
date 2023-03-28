using Dapper;
using Dapper.Contrib.Extensions;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models;
using Panta2.Infrastructure.Context;

namespace Panta2.Infrastructure
{
    public class ServiceRepository : IServiceRepository
    {
        private DapperContext _context;

        public ServiceRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.GetAllAsync<Service>();
            }
        }

        public async Task<Service> GetById(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.GetAsync<Service>(id);
            }
        }

        public async Task<Service> Add(Service service)
        {
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.InsertAsync(service);
                service.Id = id;
                return service;
            }
        }
        public async Task<bool> Update(Service service)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.UpdateAsync(service);
            }
        }

        public async Task<bool> Remove(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.DeleteAsync(new Service { Id = id });
            }
        }
    }
}
