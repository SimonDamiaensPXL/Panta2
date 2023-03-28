using Panta2.Core.Entities;
using Panta2.Core.Models;

namespace Panta2.Core.Contracts
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAll();
        Task<Service> GetById(int id);
        Task<Service> Add(Service service);
        Task<bool> Update(Service service);
        Task<bool> Remove(int id);
    }
}
