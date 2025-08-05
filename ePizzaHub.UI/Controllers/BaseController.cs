using ePizzaHub.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;

namespace ePizzaHub.UI.Controllers
{
    public class BaseController : Controller
    {
        public UserModel CurrentUser
        {
            get
            {
                if (User.Claims.Any())
                {
                    string userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)!.ToString();
                    string email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.ToString();
                    string userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")!.ToString();


                    return new UserModel
                    {
                        Email = email,
                        Name = userName,
                        UserId = Convert.ToInt32(userId),

                    };
                }
                return null;
            }
        }
    }
}
