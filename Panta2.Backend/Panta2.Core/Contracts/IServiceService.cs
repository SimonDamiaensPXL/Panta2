using Panta2.Core.Models;

namespace Panta2.Core.Contracts
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceModel>> GetServiceList();
        Task<ServiceModel> GetServiceById(int id);
        Task<ServiceModel> InsertService(ServiceCreationModel service);
        Task<bool> UpdateService(ServiceModel service);
        Task<bool> DeleteService(int id);
    }
}
