﻿using Panta2.Core.Models.Service;
using Panta2.Core.Models.User;

namespace Panta2.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetUserList();
        Task<UserModel> GetUserById(int id);
        Task<UserModel> RegisterUser(UserRegistrationModel user);
        Task<UserModel> LoginUser(string username, string password);
        Task<bool> ChangeFirstName(string username, int id);
        Task<IEnumerable<ServiceModel>> GetAllServicesFromUser(int id);
        Task<IEnumerable<ServiceModel>> GetAllFavoriteServicesFromUser(int id);
        Task<IEnumerable<SerivceWithIsFavoriteModel>> GetAllServicesWithIsFavoriteFromUser(int id);
        Task<bool> EditFavoritesFromUser(int userId, int serviceId, bool isFavorite);
    }
}
