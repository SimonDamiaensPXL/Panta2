using Panta2.Core.Models;

namespace Panta2.Core.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyModel>> GetCompanyList();
        Task<CompanyModel> GetCompanyById(int id);
        Task<string> GetCompanyLogoById(int id);
        Task<CompanyModel> InsertCompany(CompanyCreationModel company);
        Task<bool> UpdateCompany(CompanyModel company);
        Task<bool> DeleteCompany(int id);

    }
}
