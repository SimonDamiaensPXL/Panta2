using Panta2.Core.Entities;
using Panta2.Core.Models.Company;
using Panta2.Core.Models.Role;
using Panta2.Core.Models.Service;
using Panta2.Core.Models.User;

namespace Panta2.Core.Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAll();
        Task<Company> GetById(int id);
        Task<string> GetLogo(int id);
        Task<Role> GetRoleById(int id);
        Task<IEnumerable<Service>> GetServicesFromRole(int id);
        Task<IEnumerable<ServiceWithIsInRoleModel>> GetServicesFromCompanyWithIsInRole(int companyId, int roleId);
        Task<Company> Add(Company company);
        Task<bool> UpdateName(Company company);
        Task<bool> UpdateLogo(Company company);
        Task<bool> Remove(int id);
        Task<IEnumerable<UserWithRoleNameModel>> GetUsersWithRoleNameFromCompany(int id);
        Task<IEnumerable<RoleModel>> GetRolesFromCompany(int id);
        Task<int> AddNewRoleToCompany(RoleCreationModel model, int companyId);
        Task<IEnumerable<ServiceModel>> GetServicesFromCompany(int id);
        Task<ServiceModel> GetServiceFromCompany(int companyId, int serviceId);
        Task<IEnumerable<ServiceNameModel>> GetServiceNamesFromCompany(int id);
        Task<IEnumerable<ServiceNameModel>> GetServiceNamesNotInCompany(int id);
        Task<int> AddServicesToCompany(CompanyServicesCreationModel model);
        Task<bool> UpdateServiceName(Service service, int companyId);
        Task<bool> UpdateServiceIcon(Service service, int companyId);
        Task<bool> UpdateRoleName(int roleId, string name);
        Task<bool> UpdateRoleServices(int roleId, int[] serviceIds);
    }
}
