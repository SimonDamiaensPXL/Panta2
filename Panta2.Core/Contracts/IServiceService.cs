using Panta2.Core.Models;

namespace Panta2.Core.Contracts
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetServiceList();
        Task<ServiceDto> GetServiceById(int id);
        Task<ServiceDto> InsertService(ServiceForCreationDto service);
        Task<bool> UpdateService(ServiceDto service);
        Task<bool> DeleteService(int id);
    }
}
