using Microsoft.AspNetCore.Mvc;
using Panta2.Core.Contracts;
using Panta2.Core.Models;

namespace Panta2.ConfigAPI.Controllers
{
    [ApiController]
    [Route("configapi/services")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyModel>>> GetServices()
        {
            var companies = await _serviceService.GetServiceList();
            return Ok(companies);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompany(ServiceCreationModel service)
        {
            await _serviceService.InsertService(service);
            return NoContent();
        }
    }
}
