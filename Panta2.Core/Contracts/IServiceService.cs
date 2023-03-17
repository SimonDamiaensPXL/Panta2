using Panta2.Core.Models;

namespace Panta2.Core.Contracts
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDto>> GetServiceList();
        Task<ServiceDto> GetServiceById(int id);
    }
}
