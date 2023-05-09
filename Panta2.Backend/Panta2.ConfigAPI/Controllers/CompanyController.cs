using Microsoft.AspNetCore.Mvc;
using Panta2.Core.Contracts;
using Panta2.Core.Models.Company;

namespace Panta2.ConfigAPI.Controllers
{
    [ApiController]
    [Route("configapi/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyModel>>> GetCompanies()
        {
            var companies = await _companyService.GetCompanyList();
            return Ok(companies);
        }

        [HttpGet("{id}", Name = "GetCompanyById")]
        public async Task<ActionResult<CompanyModel>> GetCompanyById(int id)
        {
            var company = await _companyService.GetCompanyById(id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompany(CompanyCreationModel company)
        {
            await _companyService.InsertCompany(company);
            return NoContent();
        }

        [HttpPut("name")]
        public async Task<ActionResult> UpdateCompanyName(CompanyNameUpdateModel company)
        {
            if (!await _companyService.UpdateCompanyName(company))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("logo")]
        public async Task<ActionResult> UpdateCompanyLogo(CompanyLogoUpdateModel company)
        {
            if (!await _companyService.UpdateCompanyLogo(company))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            if (!await _companyService.DeleteCompany(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
