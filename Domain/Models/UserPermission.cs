using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using static Common.Enums.Enums;

namespace Domain.Models
{
    public class UserPermission : IdentityRole<int>
    {
        /// <summary>
        /// Collection Identity Roles
        /// </summary>
        public virtual ICollection<IdentityRoleClaim<int>> Claims { get; } = new List<IdentityRoleClaim<int>>();
        /// <summary>
        /// Collection Users
        /// </summary>
        public virtual ICollection<IdentityUserRole<int>> Users { get; } = new List<IdentityUserRole<int>>();
        /// <summary>
        /// Permissions
        /// </summary>
        public Permission Permission { get; set; }
        /// <summary>
        /// Create Date
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.Now.ToUniversalTime().AddHours(4);
        /// <summary>
        /// Update Date
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// Delete Date
        /// </summary>
        public DateTime? DeleteDate { get; set; }
    }
}
