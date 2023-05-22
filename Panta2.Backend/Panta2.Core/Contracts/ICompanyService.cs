using Panta2.Core.Models.Company;
using Panta2.Core.Models.Role;
using Panta2.Core.Models.Service;
using Panta2.Core.Models.User;

namespace Panta2.Core.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyModel>> GetCompanyList();
        Task<CompanyModel> GetCompanyById(int id);
        Task<string> GetCompanyLogoById(int id);
        Task<CompanyModel> CreateCompany(CompanyCreationModel company);
        Task<bool> UpdateCompanyName(CompanyNameUpdateModel company);
        Task<bool> UpdateCompanyLogo(CompanyLogoUpdateModel company);
        Task<bool> DeleteCompany(int id);
        Task<IEnumerable<UserWithRoleNameModel>> GetUsersWithRoleName(int id);
        Task<IEnumerable<RoleModel>> GetAllRolesFromCompany(int id);
        Task<int> CreateRole(RoleCreationModel model, int companyId);
        Task<IEnumerable<ServiceModel>> GetServiceListFromCompany(int id);
        Task<ServiceModel> GetSingleServiceFromCompany(int companyId, int serivceId);
        Task<IEnumerable<ServiceNameModel>> GetServiceNamesFromCompany(int id);
        Task<IEnumerable<ServiceNameModel>> GetServiceNamesNotInCompany(int id);
        Task<int> AddServicesToCompany(CompanyServicesCreationModel model);
        Task<bool> UpdateService(ServiceNameUpdateModel model, int companyId);
        Task<bool> UpdateService(ServiceIconUpdateModel model, int companyId);
    }
}
