using Microsoft.IdentityModel.Tokens;
using Repository.Data.DTO;
using Repository.Utilities.Config;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public static class JwtToken
    {
       public static string Create(StorePeopleDTO peopleDTO)
        {
            string SecretKey = ConfigManager.AppSetting["jwt-secret-key"]!;
            var TokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.UTF8.GetBytes(SecretKey);
            var TokenSetUp = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, peopleDTO.DESCRIPTION),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("Country_ID", "147")
                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };

            var Token = TokenHandler.CreateToken(TokenSetUp);

            return TokenHandler.WriteToken(Token);
        }
    }
}
