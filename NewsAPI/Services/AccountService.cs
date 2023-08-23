using System.Collections.Generic;
using System;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using NewsAPI.Models;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Net;

namespace NewsAPI.Services
{
    public class AccountService
    {
        private readonly IConfiguration _configuration;
        public static readonly Lazy<List<User>> Users;

        static AccountService()
        {
            Users = new Lazy<List<User>>(() => JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("Users.json")));
        }

        public AccountService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public User GetUser(string username, string password)
        {
            username = username.Trim().ToLower();
            password = password.Trim().ToLower();

            var user = Users.Value.FirstOrDefault(a => a.Username.ToLower() == username && a.Password.ToLower() == password);
            return user;
        }
        public int GetUserId(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(HeaderNames.Authorization, out StringValues authorizationHeader))
            {
                throw new Exception();
            }

            int userId = GetClaimValue<int>(authorizationHeader.ToString().Replace("Bearer ", ""), "UserId");
            return userId;

        }
        public T GetClaimValue<T>(string accessToken, string claimName)
        {
            T res;

            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);

            string value = jwtToken.Claims.First(m => m.Type == claimName).Value;

            res = (T)Convert.ChangeType(value, typeof(T));

            return res;
        }
    }
}
