using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panta2.Core.Contracts;
using Panta2.Core.Entities;
using Panta2.Core.Models.User;

namespace Panta2.ConfigAPI.Controllers
{
    [ApiController]
    [Route("configapi/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUsers()
        {
            var users = await _userService.GetUserList();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
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

        [HttpPost]
        public async Task<ActionResult> RegisterUser(UserRegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUser = await _userService.RegisterUser(model);
            return CreatedAtRoute("GetUserById", new { createdUser.Id }, createdUser);
        }

        [HttpPut("username")]
        public async Task<ActionResult> ChangeUser(UserUserNameUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isFound = await _userService.ChangeUser(model);

            if (!isFound)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("name")]
        public async Task<ActionResult> ChangeUser(UserNameUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isFound = await _userService.ChangeUser(model);

            if (!isFound)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("email")]
        public async Task<ActionResult> ChangeUser(UserEmailUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isFound = await _userService.ChangeUser(model);

            if (!isFound)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("password")]
        public async Task<ActionResult> ChangeUser(UserPasswordUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isFound = await _userService.ChangeUser(model);

            if (!isFound)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
