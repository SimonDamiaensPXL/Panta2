using Microsoft.AspNetCore.Mvc;
using Panta2.Application;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models;

namespace Panta2.API.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices()
        {
            var services = await _serviceService.GetServiceList();
            return Ok(services);
        }

        [HttpGet("{id}", Name = "GetServiceById")]
        public async Task<ActionResult<ServiceDto>> GetServiceById(int id)
        {
            var service = await _serviceService.GetServiceById(id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceDto>> CreateService(ServiceForCreationDto service)
        {
            var createdService = await _serviceService.InsertService(service);
            return CreatedAtRoute("GetServiceById", new { createdService.Id }, createdService);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceDto>> UpdateService(ServiceDto service)
        {
            if (!await _serviceService.UpdateService(service))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteService(int id)
        {
            if (!await _serviceService.DeleteService(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
