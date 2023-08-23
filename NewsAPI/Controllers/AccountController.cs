using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NewsAPI.Models;
using NewsAPI.Services;

namespace NewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AccountService _accountService;

        public AccountController(IConfiguration configuration, AccountService accountService)
        {
            _configuration = configuration;
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginInputModel dto)
        {
            var user = _accountService.GetUser(dto.Username, dto.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            string secretKey = _configuration["JWT:SecretKey"];

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("Name", user.Name)
            };

            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(double.Parse(_configuration["JWT:AccessTokenExpirationSeconds"])),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256
            ));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new { token });
        }
    }
}
