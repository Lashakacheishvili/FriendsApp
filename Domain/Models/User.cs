using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using static Common.Enums.Enums;

namespace Domain.Models
{
    public class User : IdentityUser<int>
    {
        /// <summary>
        /// User Contact Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Site Url
        /// </summary>
        public string SiteUrl { get; set; }
        /// <summary>
        /// User Status Enum
        /// </summary>
        public UserStatus UserStatus { get; set; }
        /// <summary>
        /// Registration Date
        /// </summary>
        public DateTime RegistrationDate { get; set; } = DateTime.Now.ToUniversalTime().AddHours(4);
        /// <summary>
        /// Last Update Date
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// Date of cancellation of the profile
        /// </summary>
        public DateTime? DeleteDate { get; set; }
        /// <summary>
        /// Last Authorization Date
        /// </summary>
        public DateTime? LastLoginAt { get; set; }
        /// <summary>
        /// User Roles
        /// </summary>
        public virtual ICollection<IdentityUserRole<int>> Roles { get; set; }
    }
}
