using Panta2.Core.Models;

namespace Panta2.Core.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetCompanyList();
        Task<CompanyDto> GetCompanyById(int id);
        Task<CompanyDto> InsertCompany(CompanyForCreationDto company);
    }
}
