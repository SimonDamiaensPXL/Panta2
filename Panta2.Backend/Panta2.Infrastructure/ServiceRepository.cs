using Dapper;
using Dapper.Contrib.Extensions;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models;
using Panta2.Core.Models.Role;
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
        public async Task<bool> UpdateName(Service service)
        {
            var query = "UPDATE Services SET Name = @name WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { name = service.Name, id = service.Id });
                return rowsAffected == 1;
            }
        }

        public async Task<bool> UpdateLink(Service service)
        {
            var query = "UPDATE Services SET Link = @link WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { link = service.Link, id = service.Id });
                return rowsAffected == 1;
            }
        }

        public async Task<bool> UpdateIcon(Service service)
        {
            var query = "UPDATE Services SET Icon = @icon WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { icon = service.Icon, id = service.Id });
                return rowsAffected == 1;
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
