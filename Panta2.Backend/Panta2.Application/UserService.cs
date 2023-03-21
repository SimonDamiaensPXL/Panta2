using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
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
            finalUser.PasswordHash = _passwordHasher.HashPassword(finalUser, user.Password);
            
            var createdUser = await _userRepository.CreateAsync(finalUser);

            return _mapper.Map<UserModel>(createdUser);
        }

        public async Task<UserModel> LoginUser(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);

            if (user == null) 
            {
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success ? _mapper.Map<UserModel>(user) : null;
        }
    }
}