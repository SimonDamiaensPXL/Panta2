using Microsoft.AspNetCore.Mvc;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models.Company;
using Panta2.Core.Models.Role;
using Panta2.Core.Models.User;

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
        public async Task<ActionResult> CreateCompany(CompanyCreationModel model)
        {
            await _companyService.CreateCompany(model);
            return NoContent();
        }


        [HttpPut("name")]
        public async Task<ActionResult> UpdateCompanyName(CompanyNameUpdateModel model)
        {
            if (!await _companyService.UpdateCompanyName(model))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("logo")]
        public async Task<ActionResult> UpdateCompanyLogo(CompanyLogoUpdateModel model)
        {
            if (!await _companyService.UpdateCompanyLogo(model))
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

        [HttpGet("users/{id}")]
        public async Task<ActionResult<IEnumerable<UserWithRoleNameModel>>> GetUsersWithRoleName(int id)
        {
            var companies = await _companyService.GetUsersWithRoleName(id);
            return Ok(companies);
        }

        [HttpGet("roles/{id}")]
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetAllRolesFromCompany(int id)
        {
            var roles = await _companyService.GetAllRolesFromCompany(id);
            return Ok(roles);
        }

        [HttpPost("roles/{id}")]
        public async Task<ActionResult> CreateRole(RoleCreationModel model, int id)
        {
            var newRole = await _companyService.CreateRole(model, id);

            return Ok(newRole);
        }
    }
}
