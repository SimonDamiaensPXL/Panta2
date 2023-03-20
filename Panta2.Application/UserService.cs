﻿using AutoMapper;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models;
using Panta2.Infrastructure;

namespace Panta2.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserModel> GetUserById(int id)
        {
            var userEntity = await _userRepository.GetById(id);
            return _mapper.Map<UserModel>(userEntity);
        }

        public async Task<UserModel> RegisterUser(UserRegistrationModel user)
        {
            var finalUser = _mapper.Map<User>(user);

            var createdUser = await _userRepository.CreateAsync(finalUser);

            return _mapper.Map<UserModel>(createdUser);
        }
    }
}