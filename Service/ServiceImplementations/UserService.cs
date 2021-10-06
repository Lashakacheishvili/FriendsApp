﻿using Domain;
using Microsoft.EntityFrameworkCore;
using Service.Helper;
using Service.ServiceInterfaces;
using ServiceModels;
using ServiceModels.Models.User;
using System.Linq;
using System.Threading.Tasks;

namespace Service.ServiceImplementations
{
    public class UserService : IUserService
    {
        private readonly FriendsAppDbContext _context;
        public UserService(FriendsAppDbContext context)
        {
            _context = context;
        }
        //User Information
        public async Task<UserItemModel> GetUser(UserRequestModel request)
        {
            var command = $@"Select ""Id"" ,""UserName"",""SiteUrl"" from ""Users"" u where ""DeleteDate""  isnull and u.""Id""={request.Id} 
                             {(!string.IsNullOrEmpty(request.Name)?$@" and u.""UserName"" like N'%{request.Name}%'":string.Empty)}";
            var gen = new CRUDGenerator<UserRequestModel, Domain.Models.User, UserItemModel>(request, _context.Database.GetDbConnection(), command);
            var resp = await gen.GenerateSelectAndCount();
            return resp.Item1?.FirstOrDefault();
        }
        //Update user
        public async Task<BaseResponseModel> UpdateUser(UserItemModel model)
        {
            var gen = new CRUDGenerator<UserItemModel, Domain.Models.User, BaseResponseModel>(model, _context.Database.GetDbConnection());
            return await gen.GenerateUpdate();
        }
        //Add user
        public async Task<BaseResponseModel> InsertUser(CreateUserModel model)
        {
            var gen = new CRUDGenerator<CreateUserModel, Domain.Models.User, BaseResponseModel>(model, _context.Database.GetDbConnection());
            return await gen.GenerateInsert();
        }
        //Delete user
        public async Task<BaseResponseModel> DeleteUser(int userId)
        {
            var gen = new CRUDGenerator<int?, Domain.Models.User, BaseResponseModel>(null, _context.Database.GetDbConnection());
            return await gen.GenerateSoftDelete(userId);
        }
        //Add friend
        public async Task<BaseResponseModel> AddFriend(AddFriendRequestModel request)
        {
            var gen = new CRUDGenerator<AddFriendRequestModel, Domain.Models.Friend, BaseResponseModel>(request, _context.Database.GetDbConnection());
            return await gen.GenerateInsert();
        }
    }
}
