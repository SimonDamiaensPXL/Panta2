using AutoMapper;
using Panta2.Core.Models.Company;

namespace Panta2.Core.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Entities.Company, CompanyModel>();
            CreateMap<CompanyModel, Entities.Company>();
            CreateMap<CompanyCreationModel, Entities.Company>();
            CreateMap<Entities.Company, CompanyCreationModel>();
            CreateMap<CompanyNameUpdateModel, Entities.Company>();
            CreateMap<CompanyLogoUpdateModel, Entities.Company>();
        }
    }
}
