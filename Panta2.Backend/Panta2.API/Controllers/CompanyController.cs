using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panta2.Application;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models;

namespace Panta2.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
        }

        [HttpGet("logo/{id}")]
        public async Task<ActionResult<CompanyModel>> GetCompanyLogoById(int id)
        {
            var logo = await _companyService.GetCompanyLogoById(id);

            if (logo == null)
            {
                return NotFound();
            }

            return Ok(new { logo });
        }
    }
}
