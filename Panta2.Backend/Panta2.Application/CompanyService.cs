using AutoMapper;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Imagekit.Sdk;
using Microsoft.Extensions.Configuration;
using Panta2.Core.Models.Company;
using Panta2.Core.Models.User;
using Panta2.Core.Models.Role;
using Panta2.Core.Models.Service;
using Panta2.Infrastructure;

namespace Panta2.Application
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ImagekitClient _imagekit;
        private readonly IConfiguration _configuration;

        public CompanyService(IConfiguration configuration, ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _imagekit = new ImagekitClient(
                _configuration["ImageKitSettings:PublicKey"],
                _configuration["ImageKitSettings:PrivateKey"],
                _configuration["ImageKitSettings:UrlEndpoint"]
                );
        }

        public async Task<IEnumerable<CompanyModel>> GetCompanyList()
        {
            var companyEntities = await _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyModel>>(companyEntities);
        }

        public async Task<CompanyModel> GetCompanyById(int id)
        {
            var companyEntity = await _companyRepository.GetById(id);
            return _mapper.Map<CompanyModel>(companyEntity);
        }

        public async Task<string> GetCompanyLogoById(int id)
        {
            return await _companyRepository.GetLogo(id);

        }

        public async Task<RoleModel> GetRoleById(int id)
        {
            var role = await _companyRepository.GetRoleById(id);
            return _mapper.Map<RoleModel>(role);
        }

        public async Task<IEnumerable<ServiceModel>> GetAllServicesFromRole(int id)
        {
            var serviceEntities = await _companyRepository.GetServicesFromRole(id);
            return _mapper.Map<IEnumerable<ServiceModel>>(serviceEntities);
        }

        public async Task<IEnumerable<ServiceWithIsInRoleModel>> GetAllServicesFromCompanyWithIsInRole(int companyId, int roleId)
        {
            return await _companyRepository.GetServicesFromCompanyWithIsInRole(companyId, roleId);
        }

        public async Task<CompanyModel> CreateCompany(CompanyCreationModel company)
        {
            FileCreateRequest fileCreateRequest = new FileCreateRequest
            {
                file = company.Logo,
                fileName = $"{company.Name}-{Guid.NewGuid()}",
                folder = "assets/images/logos"
            };
            Result resp = _imagekit.Upload(fileCreateRequest);

            company.Logo = resp.url;

            var finalCompany = _mapper.Map<Company>(company);

            var createdCompany = await _companyRepository.Add(finalCompany);

            return _mapper.Map<CompanyModel>(createdCompany);
        }

        public async Task<bool> UpdateCompanyName(CompanyNameUpdateModel company)
        {
            var updateCompany = _mapper.Map<Company>(company);
            return await _companyRepository.UpdateName(updateCompany);
        }

        public async Task<bool> UpdateCompanyLogo(CompanyLogoUpdateModel company)
        {
            FileCreateRequest fileCreateRequest = new FileCreateRequest
            {
                file = company.Logo,
                fileName = $"{company.Name}-{Guid.NewGuid()}",
                folder = "assets/images/logos"
            };
            Result resp = _imagekit.Upload(fileCreateRequest);

            company.Logo = resp.url;

            var updateCompany = _mapper.Map<Company>(company);
            return await _companyRepository.UpdateLogo(updateCompany);
        }

        public async Task<bool> DeleteCompany(int id)
        {
            return await _companyRepository.Remove(id);
        }

        public async Task<IEnumerable<UserWithRoleNameModel>> GetUsersWithRoleName(int id)
        {
            return await _companyRepository.GetUsersWithRoleNameFromCompany(id);
        }

        public async Task<IEnumerable<RoleModel>> GetAllRolesFromCompany(int id)
        {
            return await _companyRepository.GetRolesFromCompany(id);
        }

        public async Task<int> CreateRole(RoleCreationModel model, int companyId)
        {
            return await _companyRepository.AddNewRoleToCompany(model, companyId);
        }

        public async Task<IEnumerable<ServiceModel>> GetServiceListFromCompany(int id)
        {
            return await _companyRepository.GetServicesFromCompany(id);
        }

        public async Task<ServiceModel> GetSingleServiceFromCompany(int companyId, int serivceId)
        {
            return await _companyRepository.GetServiceFromCompany(companyId, serivceId);
        }

        public async Task<IEnumerable<ServiceNameModel>> GetServiceNamesFromCompany(int id)
        {
            var serviceEntities = await _companyRepository.GetServiceNamesFromCompany(id);
            return _mapper.Map<IEnumerable<ServiceNameModel>>(serviceEntities);
        }

        public async Task<IEnumerable<ServiceNameModel>> GetServiceNamesNotInCompany(int id)
        {
            var serviceEntities = await _companyRepository.GetServiceNamesNotInCompany(id);
            return _mapper.Map<IEnumerable<ServiceNameModel>>(serviceEntities);
        }

        public async Task<int> AddServicesToCompany(CompanyServicesCreationModel model)
        {
            return await _companyRepository.AddServicesToCompany(model);
        }

        public async Task<bool> UpdateService(ServiceNameUpdateModel model, int companyId)
        {
            var updateService = _mapper.Map<Service>(model);
            return await _companyRepository.UpdateServiceName(updateService, companyId);
        }

        public async Task<bool> UpdateService(ServiceIconUpdateModel model, int companyId)
        {
            FileCreateRequest fileCreateRequest = new FileCreateRequest
            {
                file = model.Icon,
                fileName = $"{model.Name}-{Guid.NewGuid()}",
                folder = "assets/images/icons"
            };
            Result resp = _imagekit.Upload(fileCreateRequest);

            model.Icon = resp.url;

            var updateCompany = _mapper.Map<Service>(model);
            return await _companyRepository.UpdateServiceIcon(updateCompany, companyId);
        }

        public async Task<bool> UpdateRole(int roleId, string name)
        {
            return await _companyRepository.UpdateRoleName(roleId, name);
        }
        public async Task<bool> UpdateRole(int roleId, int[] serviceIds)
        {
            return await _companyRepository.UpdateRoleServices(roleId, serviceIds);
        }
    }
}
