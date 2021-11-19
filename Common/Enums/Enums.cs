using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enums
{
    public class Enums
    {
        /// <summary>
        /// User Statuses
        /// </summary>
        public enum UserStatus
        {
            Active = 0,
            Block = 1,
            Delete = 2
        }
        /// <summary>
        /// User Permissions
        /// </summary>
        [Flags]
        public enum Permission
        {
            None = 0
        }
        /// <summary>
        /// User Roles
        /// </summary>
        public enum UserRole
        {
            User = 0
        }
        public enum Gender
        {
            Male = 0,
            Female = 1
        }
    }
}
