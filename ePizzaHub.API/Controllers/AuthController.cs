using ePizzaHub.Core.Contracts;
using ePizzaHub.Models.ApiModels.Response;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase

    {
        private readonly IAuthService _authService;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthService authService, ITokenGeneratorService tokenGeneratorService ,IConfiguration configuration)
        {
            _authService = authService;
            _tokenGeneratorService = tokenGeneratorService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> ValidateUser(string username,string password)
        {
            var useDetails = await _authService.ValidateUserAsync(username, password);
            if (useDetails.UserId > 0)
            {
                var accessToken = _tokenGeneratorService.GenerateToken(useDetails);

                var authapiResponse
                     = new AuthApiResponse()
                     {
                         AccessToken = accessToken,
                         TokenExpiryInMinutes = Convert.ToInt32(_configuration["Jwt:TokenExpiryInMinutes"])
                     };

                return Ok(authapiResponse);
            }
            return Ok(useDetails);
        }
    }
}
