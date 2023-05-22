using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Panta2.Application;
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

        [HttpGet("{companyId}/service/{serviceId}")]
        public async Task<ActionResult<ServiceModel>> GetSingleServicesFromCompany(int companyId, int serviceId)
        {
            var service = await _companyService.GetSingleServiceFromCompany(companyId, serviceId);
            return Ok(service);
        }

        [HttpGet("service-names/{id}")]
        public async Task<ActionResult<IEnumerable<ServiceNameModel>>> GetAllServiceNamesFromCompany(int id)
        {
            var roles = await _companyService.GetServiceNamesFromCompany(id);
            return Ok(roles);
        }

        [HttpGet("service-names-not-in/{id}")]
        public async Task<ActionResult<IEnumerable<ServiceNameModel>>> GetAllServiceNamesNotInCompany(int id)
        {
            var roles = await _companyService.GetServiceNamesNotInCompany(id);
            return Ok(roles);
        }

        [HttpPost("company-services")]
        public async Task<ActionResult> AddCompanyServices(CompanyServicesCreationModel model)
        {
            if (model.Services.IsNullOrEmpty())
            {
                return BadRequest(new { message = "No services were added!" });
            }

            var companyId = await _companyService.AddServicesToCompany(model);

            return Ok(companyId);
        }

        [HttpPut("{companyId}/service-name")]
        public async Task<ActionResult> UpdateServiceName(ServiceNameUpdateModel model, int companyId)
        {
            if (!await _companyService.UpdateService(model, companyId))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{companyId}/service-icon")]
        public async Task<ActionResult> UpdateServiceIcon(ServiceIconUpdateModel model, int companyId)
        {
            if (!await _companyService.UpdateService(model, companyId))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
