using ePizzaHub.Core.Contracts;
using ePizzaHub.Models.ApiModels.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace ePizzaHub.Core.Concrete
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly IConfiguration configuration;

        public TokenGeneratorService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public string GenerateToken(ValidateUserResponse userResponse)
        {
            string secret = configuration["Jwt:Secret"]!;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDesriptor
                 = new SecurityTokenDescriptor
                 {
                     Subject = new System.Security.Claims.ClaimsIdentity([
                          new Claim(ClaimTypes.Name,userResponse.Name),
                          new Claim(ClaimTypes.Email,userResponse.Email),
                          new Claim("UserId",userResponse.UserId.ToString()),
                          new Claim("IsAdmin",userResponse.Roles.Any(x => x.Equals("Admin")).ToString()),
                          new Claim("Roles",JsonSerializer.Serialize(userResponse.Roles))
                         ]),
                     Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["Jwt:TokenExpiryInMinutes"])),
                     SigningCredentials = credentials,
                     Issuer = configuration["Jwt:Issuer"],
                     Audience = configuration["Jwt:Audience"]
                 };

            var tokenHandler
                = new JsonWebTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesriptor);
            return token;
        }
    }
}
