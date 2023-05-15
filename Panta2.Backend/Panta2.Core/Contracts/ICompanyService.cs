using Panta2.Core.Models.Company;
using Panta2.Core.Models.Role;
using Panta2.Core.Models.User;

namespace Panta2.Core.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyModel>> GetCompanyList();
        Task<CompanyModel> GetCompanyById(int id);
        Task<string> GetCompanyLogoById(int id);
        Task<CompanyModel> InsertCompany(CompanyCreationModel company);
        Task<bool> UpdateCompanyName(CompanyNameUpdateModel company);
        Task<bool> UpdateCompanyLogo(CompanyLogoUpdateModel company);
        Task<bool> DeleteCompany(int id);
        Task<IEnumerable<UserWithRoleNameModel>> GetUsersWithRoleName(int id);
        Task<IEnumerable<RoleModel>> GetAllRolesFromCompany(int id);

    }
}
