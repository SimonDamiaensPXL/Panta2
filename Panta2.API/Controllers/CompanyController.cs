using Microsoft.AspNetCore.Mvc;
using Panta2.Application;
using Panta2.Core.Contracts;
using Panta2.Core.Models;

namespace Panta2.API.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
            var companies = await _companyService.GetCompanyList();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompanyById(int id)
        {
            var service = await _companyService.GetCompanyById(id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompany(CompanyForCreationDto company)
        {
            var createdCompany = await _companyService.InsertCompany(company);
            return CreatedAtRoute("GetCompanyById", createdCompany);
        }
    }
}
