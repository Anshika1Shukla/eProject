using ePizzaHub.Core.Contracts;
using ePizzaHub.Models.ApiModels.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace ePizzaHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase

    {
        public readonly IUserService _userService;
        public UserController(IUserService userService) {
            this._userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest userRequest)

        {
            var result =  await _userService.CreateUserRequestAsync(userRequest);
            return Ok(result);
        }

    }
}
