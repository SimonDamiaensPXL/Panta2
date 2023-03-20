using AutoMapper;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models;
using Panta2.Infrastructure;

namespace Panta2.Application
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

        public async Task<CompanyModel> InsertCompany(CompanyCreationModel company)
        {
            var finalCompany = _mapper.Map<Company>(company);

            var createdCompany = await _companyRepository.Add(finalCompany);

            return _mapper.Map<CompanyModel>(createdCompany);
        }

        public async Task<bool> UpdateCompany(CompanyModel company)
        {
            var updateCompany = _mapper.Map<Company>(company);
            return await _companyRepository.Update(updateCompany);
        }

        public async Task<bool> DeleteCompany(int id)
        {
            return await _companyRepository.Remove(id);
        }
    }
}
