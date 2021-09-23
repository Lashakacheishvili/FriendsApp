using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsApi.Models
{
    public class CreateUserApiModel
    {
        public string UserName { get; set; }
        public string SiteUrl { get; set; }
        public string Password { get; set; }
    }
}
