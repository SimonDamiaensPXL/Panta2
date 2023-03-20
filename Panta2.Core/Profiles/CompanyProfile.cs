using AutoMapper;

namespace Panta2.Core.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Entities.Company, Models.CompanyModel>();
            CreateMap<Models.CompanyCreationModel, Entities.Company>();
            CreateMap<Entities.Company, Models.CompanyCreationModel>();
            CreateMap<Models.CompanyModel, Entities.Company>();
        }
    }
}
