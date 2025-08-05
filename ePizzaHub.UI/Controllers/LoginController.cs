using ePizzaHub.UI.Models.Response;
using ePizzaHub.UI.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ePizzaHub.UI.Models.Request;
using System.IdentityModel.Tokens.Jwt;

namespace ePizzaHub.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public LoginController (IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (ModelState.IsValid)
            {
                var client = httpClientFactory.CreateClient("ePizzaAPI");

                var userDetails = await client.GetFromJsonAsync<ApiResponseModel<ValidateUserResponse>>(
                                            $"Auth?userName={request.EmailAddress}&password={request.Password}");

                if (userDetails.Success)
                {

                    var accessToken = userDetails.Data.AccessToken;


                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenDetails = tokenHandler.ReadJwtToken(accessToken) as JwtSecurityToken;

                    List<Claim> claims = new List<Claim>();

                    foreach (var item in tokenDetails.Claims)
                    {
                        claims.Add(new Claim(item.Type, item.Value));
                    }


                    await GenerateTicket(claims);


                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel request)
        {
            if (ModelState.IsValid)
            {
                var client = httpClientFactory.CreateClient("ePizzaAPI");

                var userRequest
                     = new CreateUserRequest()
                     {
                         Email = request.Email,
                         Name = request.UserName,
                         Password = request.Password,
                         PhoneNumber = request.PhoneNumber
                     };

                HttpResponseMessage? userDetails = await client.PostAsJsonAsync<CreateUserRequest>("User", userRequest);
                userDetails.EnsureSuccessStatusCode();


            }
            return View();
        }

        private async Task GenerateTicket(List<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties()
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
                });
        }

    }
}

 