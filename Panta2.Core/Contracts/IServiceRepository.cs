using Panta2.Core.Entities;

namespace Panta2.Core.Contracts
{
    public interface IServiceRepository
    {
        List<Service> GetAll();
        Service GetById(int id);
        Service Add(Service service);
        Service Update(Service service);
        void Remove(int id);
    }
}
