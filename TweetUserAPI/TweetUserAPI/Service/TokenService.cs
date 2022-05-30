using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TweetUserAPI.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace TweetUserAPI.Service
{
    public class TokenService:ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string GenerateToken(User user)
        {
            string role = "User";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,role),
                new Claim("EmailId", user.Email),
                new Claim("Password",user.Password)
            };
            var byteArray = Encoding.UTF8.GetBytes(configuration["Token:SecretKey"]);
            var userSymmetricSecurityKey = new SymmetricSecurityKey(byteArray);
            var userSigningCredentials = new SigningCredentials(userSymmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            var userJwtSecurityToken = new JwtSecurityToken(
                issuer: configuration["Token:Issuer"],
                audience: configuration["Token:Audience"],
                signingCredentials: userSigningCredentials,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15)
                );

            var userJwtSecurityTokenHandler = new { token = new JwtSecurityTokenHandler().WriteToken(userJwtSecurityToken), username = user.Username, email = user.Email };
            var jsonTokenObject = JsonConvert.SerializeObject(userJwtSecurityTokenHandler);
            return jsonTokenObject;
        }
    }
}
