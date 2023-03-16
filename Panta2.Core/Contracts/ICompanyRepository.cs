﻿using Panta2.Core.Entities;

namespace Panta2.Core.Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAll();
        Company GetById(int id);
        Company Add(Company service);
        Company Update(Company service);
        void Remove(int id);
    }
}
