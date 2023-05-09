using Microsoft.AspNetCore.Mvc;
using Panta2.Core.Contracts;
using Panta2.Core.Models.Service;

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
        public async Task<ActionResult<IEnumerable<ServiceModel>>> GetServices()
        {
            var companies = await _serviceService.GetServiceList();
            return Ok(companies);
        }

        [HttpGet("{id}", Name = "GetServiceById")]
        public async Task<ActionResult<ServiceModel>> GetServiceById(int id)
        {
            var service = await _serviceService.GetServiceById(id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        [HttpPost]
        public async Task<ActionResult> CreateService(ServiceCreationModel model)
        {
            await _serviceService.InsertService(model);
            return NoContent();
        }

        [HttpPut("name")]
        public async Task<ActionResult> UpdateServiceName(ServiceNameUpdateModel model)
        {
            if (!await _serviceService.UpdateService(model))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("link")]
        public async Task<ActionResult> UpdateServiceLink(ServiceLinkUpdateModel model)
        {
            if (!await _serviceService.UpdateService(model))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("icon")]
        public async Task<ActionResult> UpdateServiceIcon(ServiceIconUpdateModel model)
        {
            if (!await _serviceService.UpdateService(model))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
