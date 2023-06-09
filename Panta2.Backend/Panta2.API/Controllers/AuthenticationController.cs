﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Panta2.Core.Contracts;
using Panta2.Core.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Panta2.API.Controllers
{
    [Route("api/auth")]
    [ApiController]

    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("signin")]
        public async Task<ActionResult> Login(UserLoginModel model)
        {
            var user = await _userService.LoginUser(model.UserName, model.Password);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user == null)
            {
                return Unauthorized();
            }

            var jwtToken = BuildJwtToken(user);

            Response.Cookies.Append("jwtToken", jwtToken, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new { 
                token = jwtToken,
                id = user.Id
            });
        }

        private string BuildJwtToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("given_name", user.FirstName),
                new Claim("family_name", user.LastName),
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(200),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
               .WriteToken(jwtSecurityToken);

            return tokenToReturn;
        }
    }
}
