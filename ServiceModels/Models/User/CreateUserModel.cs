using System;
using System.Collections.Generic;
using System.Text;
using static Common.Enums.Enums;

namespace ServiceModels.Models.User
{
    public class CreateUserModel
    {
        public string UserName { get; set; }
        public string SiteUrl { get; set; }
        public string PasswordHash { get; set; }
        public bool EmailConfirmed { get; set; } = true;
        public bool PhoneNumberConfirmed { get; set; } = true;
        public bool TwoFactorEnabled { get; set; } = false;
        public bool LockoutEnabled { get; set; } = true;
        public int AccessFailedCount { get; set; }
        public UserStatus UserStatus { get; set; }
        public DateTime RegistrationDate { get; set; }= DateTime.Now.ToUniversalTime().AddHours(4);
    }
}
