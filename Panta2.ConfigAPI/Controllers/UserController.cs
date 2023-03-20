﻿using Microsoft.AspNetCore.Mvc;
using Panta2.Core.Contracts;
using Panta2.Core.Models;

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
        public async Task<ActionResult> RegisterUser(UserRegistrationModel user)
        {
            var createdUser = await _userService.RegisterUser(user);
            return CreatedAtRoute("GetUserById", new { createdUser.Id }, createdUser);
        }
    }
}
