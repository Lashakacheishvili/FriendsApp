using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FriendsApi.Controllers
{
    public class BaseController : Controller
    {
        protected string UserIdClaim() => User?.Claims?.FirstOrDefault(a => a.Type == "sub" || a.Type == ClaimTypes.NameIdentifier)?.Value;
        protected int? UserId => int.TryParse(UserIdClaim(), out var id) ? (int?)id : null;
        protected string GetHeaderLanguage()
        {
            return Request.Headers["accept-language"].ToString();
        }
    }
}
