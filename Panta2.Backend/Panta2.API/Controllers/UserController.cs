using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panta2.Application;
using Panta2.Core.Contracts;
using Panta2.Core.Models;

namespace Panta2.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("name")]
        public async Task<ActionResult> ChangeFirstName(UpdateFirstNameModel model)
        {
            var isFound = await _userService.ChangeFirstName(model.FirstName, model.UserId);

            if (!isFound)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("services/{id}")]
        public async Task<ActionResult<IEnumerable<ServiceModel>>> GetAllServicesFromUser(int id)
        {
            var services = await _userService.GetAllServicesFromUser(id);

            if (services == null)
            {
                return NotFound();
            }

            return Ok(services);
        }

        [HttpGet("favorites/{id}")]
        public async Task<ActionResult<IEnumerable<SerivceWithIsFavoriteModel>>> GetAllFavoriteServicesFromUser(int id)
        {
            var services = await _userService.GetAllFavoriteServicesFromUser(id);

            if (services == null)
            {
                return NotFound();
            }

            return Ok(services);
        }

        [HttpGet("isfavorites/{id}")]
        public async Task<ActionResult<IEnumerable<ServiceModel>>> GetAllServicesWithIsFavoriteFromUser(int id)
        {
            var services = await _userService.GetAllServicesWithIsFavoriteFromUser(id);
            if (services == null)
            {
                return NotFound();
            }

            return Ok(services);
        }

        [HttpPut("favorite")]
        public async Task<ActionResult<bool>> EditFavoritesFromUser(EditFavoriteFromUserModel model)
        {
            bool isEdited = await _userService.EditFavoritesFromUser(model.UserId, model.ServiceId, model.IsFavorite);
            if (!isEdited)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
