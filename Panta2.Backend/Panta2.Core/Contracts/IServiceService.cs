using Panta2.Core.Models.Service;

namespace Panta2.Core.Contracts
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceModel>> GetServiceList();
        Task<ServiceModel> GetServiceById(int id);
        Task<ServiceModel> InsertService(ServiceCreationModel service);
        Task<bool> UpdateService(ServiceNameUpdateModel service);
        Task<bool> UpdateService(ServiceLinkUpdateModel service);
        Task<bool> UpdateService(ServiceIconUpdateModel service);
        Task<bool> DeleteService(int id);
    }
}
