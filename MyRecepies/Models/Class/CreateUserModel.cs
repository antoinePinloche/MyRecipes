﻿using Microsoft.AspNetCore.Identity;
using MyRecepies.web.Models.Enum;

namespace MyRecipes.web.Models.Class
{
    public class CreateUserModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
