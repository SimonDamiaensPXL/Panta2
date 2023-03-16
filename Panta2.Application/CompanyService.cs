using AutoMapper;
using Panta2.Core.Contracts;
using Panta2.Core.Models;

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

        public async Task<IEnumerable<CompanyDto>> GetCompanyList()
        {
            var companyEntities = await _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        }
    }
}
