using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models.Company;
using Panta2.Core.Models.Role;
using Panta2.Core.Models.Service;
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
            if (model.Services.IsNullOrEmpty())
            {
                return BadRequest(new { message = "No services were added to the role!" });
            }

            var roleId = await _companyService.CreateRole(model, id);

            if (roleId == 0)
            {
                return Conflict(new { message = "Given role already exists!" });
            }

            return Ok(roleId);
        }

        [HttpGet("services/{id}")]
        public async Task<ActionResult<IEnumerable<ServiceModel>>> GetAllServicesFromCompany(int id)
        {
            var services = await _companyService.GetServiceListFromCompany(id);
            return Ok(services);
        }

        [HttpGet("servicenames/{id}")]
        public async Task<ActionResult<IEnumerable<ServiceNameModel>>> GetAllServiceNamesFromCompany(int id)
        {
            var roles = await _companyService.GetServiceNamesFromCompany(id);
            return Ok(roles);
        }
    }
}
