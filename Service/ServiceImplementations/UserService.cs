using Domain;
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
        #region Get
        //User Information
        public async Task<UserItemModel> GetUser(UserRequestModel request)
        {
            var gen = new CRUDGenerator<UserRequestModel, Domain.Models.User, UserItemModel>(request, _context.Database.GetDbConnection());
            var resp = await gen.GenerateSelectAndCount();
            return resp.List?.FirstOrDefault();
        }
        public async Task<UserAnimalsListResponse> GetUserAnimals(UserAnimalsListRequest request)
        {
            var gen = new CRUDGenerator<UserAnimalsListRequest, Domain.Models.UserAnimal, UserAnimalsListItems>(request, _context.Database.GetDbConnection());
            var resp = await gen.GenerateSelectAndCount(generationWhere:true);
            return new UserAnimalsListResponse
            {
                Animals = resp.List,
                TotalCount = resp.TotalCount
            };
        }
        #endregion
        #region Update
        //Update user
        public async Task<BaseResponseModel> UpdateUser(UserItemModel model)
        {
            var gen = new CRUDGenerator<UserItemModel, Domain.Models.User, BaseResponseModel>(model, _context.Database.GetDbConnection());
            return await gen.GenerateUpdate();
        }
        #endregion
        #region Create Or Insert
        //Add user
        public async Task<BaseResponseModel> InsertUser(CreateUserModel model)
        {
            var gen = new CRUDGenerator<CreateUserModel, Domain.Models.User, BaseResponseModel>(model, _context.Database.GetDbConnection());
            return await gen.GenerateInsert();
        }
        //Add friend
        public async Task<BaseResponseModel> AddFriend(AddFriendRequestModel request)
        {
            var gen = new CRUDGenerator<AddFriendRequestModel, Domain.Models.Friend, BaseResponseModel>(request, _context.Database.GetDbConnection());
            return await gen.GenerateInsert();
        }
        #endregion
        #region Delete
        //Delete user
        public async Task<BaseResponseModel> DeleteUser(int userId)
        {
            var gen = new CRUDGenerator<int?, Domain.Models.User, BaseResponseModel>(null, _context.Database.GetDbConnection());
            return await gen.GenerateSoftDelete(userId);
        }
        #endregion
    }
}
