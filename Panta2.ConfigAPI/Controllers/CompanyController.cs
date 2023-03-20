using Microsoft.AspNetCore.Mvc;
using Panta2.Core.Contracts;
using Panta2.Core.Models;

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
            var service = await _companyService.GetCompanyById(id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompany(CompanyCreationModel company)
        {
            var createdCompany = await _companyService.InsertCompany(company);
            return CreatedAtRoute("GetCompanyById", new { createdCompany.Id }, createdCompany);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCompany(CompanyModel company)
        {
            if (!await _companyService.UpdateCompany(company))
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
