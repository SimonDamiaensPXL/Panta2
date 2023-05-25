using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models.Role;
using Panta2.Core.Models.Service;
using Panta2.Core.Models.User;
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

        public async Task<IEnumerable<UserModel>> GetUserList()
        {
            var userEntities = await _userRepository.GetAll();
            return _mapper.Map<IEnumerable<UserModel>>(userEntities);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<UserModel> RegisterUser(UserRegistrationModel user)
        {
            var finalUser = _mapper.Map<User>(user);
            finalUser.PasswordHash = _passwordHasher.HashPassword(finalUser, user.Password);
            
            var createdUser = await _userRepository.CreateAsync(finalUser, user.RoleId);

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

        public async Task<bool> ChangeFirstName(string username, int id)
        {
            return await _userRepository.UpdateFirstName(username, id);
        }

        public async Task<bool> ChangeUser(UserUserNameUpdateModel model)
        {
            return await _userRepository.UpdateUser(model);
        }
        public async Task<bool> ChangeUser(UserNameUpdateModel model)
        {
            return await _userRepository.UpdateUser(model);
        }
        public async Task<bool> ChangeUser(UserEmailUpdateModel model)
        {
            return await _userRepository.UpdateUser(model);
        }
        public async Task<bool> ChangeUser(UserPasswordUpdateModel model)
        {
            User user = await GetUserById(model.Id);
            var hashedPassword = _passwordHasher.HashPassword(user, model.Password);

            model.Password = hashedPassword;

            return await _userRepository.UpdateUser(model);
        }
        public async Task<bool> ChangeUser(UserRoleUpdateModel model)
        {
            return await _userRepository.UpdateUser(model);
        }

        public async Task<RoleModel> GetRoleFromUser(int id)
        {
            var role = await _userRepository.GetRoleFromUser(id);
            return _mapper.Map<RoleModel>(role);
        }

        public async Task<IEnumerable<ServiceModel>> GetAllServicesFromUser(int id)
        {
            var serviceEntities = await _userRepository.GetServicesFromUser(id);
            return _mapper.Map<IEnumerable<ServiceModel>>(serviceEntities);
        }

        public async Task<IEnumerable<ServiceModel>> GetAllFavoriteServicesFromUser(int id)
        {
            var serviceEntities = await _userRepository.GetFavoriteServicesFromUser(id);
            return _mapper.Map<IEnumerable<ServiceModel>>(serviceEntities);
        }

        public async Task<IEnumerable<SerivceWithIsFavoriteModel>> GetAllServicesWithIsFavoriteFromUser(int id)
        {
            return await _userRepository.GetServicesWithIsFavorite(id);
        }

        public async Task<bool> EditFavoritesFromUser(int userId, int serviceId, bool isFavorite)
        {
            if (isFavorite)
            {
                return await _userRepository.RemoveFavoriteService(userId, serviceId);
            }
            else
            {
                var favoriteServices = await GetAllFavoriteServicesFromUser(userId);
                if (favoriteServices.Count() >= 5)
                {
                    return false;
                }

                return await _userRepository.AddFavoriteService(userId, serviceId);
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _userRepository.RemoveUser(id);
        }
    }
}