using AutoMapper;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Imagekit.Sdk;
using Microsoft.Extensions.Configuration;
using Panta2.Core.Models.Company;
using Panta2.Core.Models.User;
using Panta2.Core.Models.Role;

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

        public async Task<RoleModel> CreateRole(RoleCreationModel model, int companyId)
        {
            var newRole = await _companyRepository.AddNewRoleToCompany(model, companyId);
            return _mapper.Map<RoleModel>(newRole);
        }
    }
}
