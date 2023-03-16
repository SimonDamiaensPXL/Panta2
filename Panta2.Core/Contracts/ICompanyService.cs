using Panta2.Core.Models;

namespace Panta2.Core.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetCompanyList();
    }
}
