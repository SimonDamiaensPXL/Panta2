﻿using Panta2.Core.Entities;
using Panta2.Core.Models;

namespace Panta2.Core.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<User> CreateAsync(User user);
    }
}
