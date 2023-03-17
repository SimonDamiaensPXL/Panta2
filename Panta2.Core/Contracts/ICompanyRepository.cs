using Panta2.Core.Entities;

namespace Panta2.Core.Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAll();
        Task<Company> GetById(int id);
        Task<Company> Add(Company company);
        Task<bool> Update(Company company);
        Task<bool> Remove(int id);
    }
}
