using AutoMapper;
using Imagekit.Sdk;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models;

namespace Panta2.Application
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ImagekitClient _imagekit;
        private readonly string publicKey = "public_JqyPbaZd1wHPLPsXWryER3v48vw=";
        private readonly string urlEndPoint = "https://ik.imagekit.io/panta2/";
        private readonly string privateKey = "private_OBkUkzNy0p7AzRqoGBrz2tvSpAc=";

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _imagekit = new ImagekitClient(publicKey, privateKey, urlEndPoint);
        }

        public async Task<IEnumerable<ServiceModel>> GetServiceList()
        {
            var serviceEntities = await _serviceRepository.GetAll();
            return _mapper.Map<IEnumerable<ServiceModel>>(serviceEntities);
        }

        public async Task<ServiceModel> GetServiceById(int id)
        {
            var serviceEntity = await _serviceRepository.GetById(id);
            return _mapper.Map<ServiceModel>(serviceEntity);
        }

        public async Task<ServiceModel> InsertService(ServiceCreationModel service)
        {
            FileCreateRequest ob2 = new FileCreateRequest
            {
                file = service.Icon,
                fileName = $"{service.Name}-{Guid.NewGuid()}",
                folder = "assets/images/icons"
            };
            Result resp = _imagekit.Upload(ob2);

            service.Icon = resp.url;

            var finalService = _mapper.Map<Service>(service);

            var createdService = await _serviceRepository.Add(finalService);

            return _mapper.Map<ServiceModel>(createdService);
        }

        public async Task<bool> UpdateService(ServiceModel service)
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