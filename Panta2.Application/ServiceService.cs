using AutoMapper;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models;
using Panta2.Infrastructure;

namespace Panta2.Application
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ServiceDto>> GetServiceList()
        {
            var serviceEntities = await _serviceRepository.GetAll();
            return _mapper.Map<IEnumerable<ServiceDto>>(serviceEntities);
        }

        public async Task<ServiceDto> GetServiceById(int id)
        {
            var serviceEntity = await _serviceRepository.GetById(id);
            return _mapper.Map<ServiceDto>(serviceEntity);
        }

        public async Task<ServiceDto> InsertService(ServiceForCreationDto service)
        {
            var finalService = _mapper.Map<Service>(service);

            var createdService = await _serviceRepository.Add(finalService);

            return _mapper.Map<ServiceDto>(createdService);
        }

        public async Task<bool> UpdateService(ServiceDto service)
        {
            var updateService = _mapper.Map<Service>(service);
            return await _serviceRepository.Update(updateService);
        }

        public async Task<bool> DeleteService(int id)
        {
            return await _serviceRepository.Remove(id);
        }
    }
}

