using FriendsApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces;
using ServiceModels;
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
        #region Gets Method
        /// <summary>
        /// User info
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("user")]
        [Authorize(Policy = "FriendsApi")]
        public async Task<UserItemModel> GetUser(UserRequestModel request)
        {
            request.Id = (request.Id.HasValue ? request.Id : UserId);
            return await _userService.GetUser(request);
        }
        #endregion
        #region Posts Method
        /// <summary>
        /// Insert User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("add_user")]
        [Authorize(Policy = "FriendsApi")]
        public async Task<BaseResponseModel> AddUser([FromBody] CreateUserApiModel request)
        {
            return await _userService.InsertUser(new CreateUserModel { SiteUrl = request.SiteUrl, UserName = request.UserName, PasswordHash = request.Password });
        }
        [HttpPost("add-friend/{id}")]
        [Authorize(Policy = "FriendsApi")]
        public async Task<BaseResponseModel> AddFriend(int id)
        {
            if (UserId.GetValueOrDefault() > 0)
                return await _userService.AddFriend(new AddFriendRequestModel { UserId = UserId.Value, ReceiverUserId = id });
            return new BaseResponseModel();
        }
        #endregion
        #region Put Method
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update_user")]
        [Authorize(Policy = "FriendsApi")]
        public async Task<BaseResponseModel> UpdateUser([FromBody] UserItemModel request)
        {
            if (UserId.GetValueOrDefault() < 1)
                return new BaseResponseModel();
            request.Id = UserId.Value;
            return await _userService.UpdateUser(request);
        }
        #endregion
        #region Delete Method
        /// <summary>
        /// Delete Account
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete_user")]
        [Authorize(Policy = "FriendsApi")]
        public async Task<BaseResponseModel> DeleteUser()
        {
            if (UserId.GetValueOrDefault() > 0)
                return await _userService.DeleteUser(UserId.Value);
            return new BaseResponseModel();
        }
        #endregion
    }
}
