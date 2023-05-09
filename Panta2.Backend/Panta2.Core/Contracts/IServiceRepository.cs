using Panta2.Core.Entities;

namespace Panta2.Core.Contracts
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAll();
        Task<Service> GetById(int id);
        Task<Service> Add(Service service);
        Task<bool> UpdateName(Service service);
        Task<bool> UpdateLink(Service service);
        Task<bool> UpdateIcon(Service service);
        Task<bool> Remove(int id);
    }
}
