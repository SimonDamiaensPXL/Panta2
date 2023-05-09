using Panta2.Core.Entities;
using Panta2.Core.Models;

namespace Panta2.Core.Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAll();
        Task<Company> GetById(int id);
        Task<string> GetLogo(int id);
        Task<Company> Add(Company company);
        Task<bool> UpdateName(Company company);
        Task<bool> UpdateLogo(Company company);
        Task<bool> Remove(int id);
    }
}
