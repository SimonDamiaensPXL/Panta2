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

        [HttpGet("favorite")]
        public async Task<ActionResult<IEnumerable<CompanyModel>>> GetFavoriteCompanies()
        {
            var favoriteCompanies = await _companyService.GetFavoriteCompanies();
            return Ok(favoriteCompanies);
        }
    }
}
