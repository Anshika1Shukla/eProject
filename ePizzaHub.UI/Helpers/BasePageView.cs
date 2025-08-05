﻿using ePizzaHub.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Claims;

namespace ePizzaHub.UI.Helpers
{
    public abstract class BasePageView<TModel> : RazorPage<TModel>
    {
        public UserModel CurrentUser
        {

            get
            {
                if (User.Claims.Any())
                {
                    string userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)!.Value.ToString();
                    string email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value.ToString();
                    string userId = User.Claims.FirstOrDefault(x => x.Type == "UserId")!.Value.ToString();


                    return new UserModel
                    {
                        Email = email,
                        Name = userName,
                       // UserId = Convert.ToInt32(userId)
                    };
                }
                return null;

            }
        }
    }
}
