using AutoMapper;
using Imagekit.Sdk;
using Microsoft.Extensions.Configuration;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models.Service;
using Panta2.Infrastructure;
using System.Configuration;

namespace Panta2.Application
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ImagekitClient _imagekit;
        private readonly IConfiguration _configuration;


        public ServiceService(IConfiguration configuration, IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _imagekit = new ImagekitClient(
                _configuration["ImageKitSettings:PublicKey"],
                _configuration["ImageKitSettings:PrivateKey"],
                _configuration["ImageKitSettings:UrlEndpoint"]
                );
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

        public async Task<ServiceModel> InsertService(ServiceCreationModel model)
        {
            FileCreateRequest ob2 = new FileCreateRequest
            {
                file = model.Icon,
                fileName = $"{model.Name}-{Guid.NewGuid()}",
                folder = "assets/images/icons"
            };
            Result resp = _imagekit.Upload(ob2);

            model.Icon = resp.url;

            var finalService = _mapper.Map<Service>(model);

            var createdService = await _serviceRepository.Add(finalService);

            return _mapper.Map<ServiceModel>(createdService);
        }

        public async Task<bool> UpdateService(ServiceNameUpdateModel model)
        {
            var updateService = _mapper.Map<Service>(model);
            return await _serviceRepository.UpdateName(updateService);
        }

        public async Task<bool> UpdateService(ServiceLinkUpdateModel model)
        {
            var updateService = _mapper.Map<Service>(model);
            return await _serviceRepository.UpdateLink(updateService);
        }

        public async Task<bool> UpdateService(ServiceIconUpdateModel model)
        {
            FileCreateRequest fileCreateRequest = new FileCreateRequest
            {
                file = model.Icon,
                fileName = $"{model.Name}-{Guid.NewGuid()}",
                folder = "assets/images/icons"
            };
            Result resp = _imagekit.Upload(fileCreateRequest);

            model.Icon = resp.url;

            var updateCompany = _mapper.Map<Service>(model);
            return await _serviceRepository.UpdateIcon(updateCompany);
        }

        public async Task<bool> DeleteService(int id)
        {
            return await _serviceRepository.Remove(id);
        }
    }
}