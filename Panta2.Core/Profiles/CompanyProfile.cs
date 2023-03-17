using AutoMapper;

namespace Panta2.Core.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Entities.Company, Models.CompanyDto>();
            CreateMap<Models.CompanyForCreationDto, Entities.Company>();
            CreateMap<Entities.Company, Models.CompanyForCreationDto>();
        }
    }
}
