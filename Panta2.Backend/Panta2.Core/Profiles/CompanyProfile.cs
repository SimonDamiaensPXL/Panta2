using AutoMapper;
using Panta2.Core.Entities;
using Panta2.Core.Models.Company;
using Panta2.Core.Models.Role;

namespace Panta2.Core.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyModel>();
            CreateMap<CompanyModel, Company>();
            CreateMap<CompanyCreationModel, Company>();
            CreateMap<Company, CompanyCreationModel>();
            CreateMap<CompanyNameUpdateModel, Company>();
            CreateMap<CompanyLogoUpdateModel, Company>();
            CreateMap<Role, RoleModel>();
        }
    }
}
