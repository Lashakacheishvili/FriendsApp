using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces;
using ServiceModels.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : BaseController
    {
        readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// User info
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("User")]
        [Authorize(Policy = "FriendsApi")]
        public async Task<UserItemModel> GetUser()
        {
            return await _userService.GetUser(new ServiceModels.Models.User.UserRequestModel { Id=UserId });
        }
    }
}
