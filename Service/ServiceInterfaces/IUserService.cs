using ServiceModels;
using ServiceModels.Models.User;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserItemModel> GetUser(UserRequestModel request);
        Task<BaseResponseModel> InsertUser(CreateUserModel model);
        Task<BaseResponseModel> UpdateUser(UserItemModel model);
        Task<BaseResponseModel> DeleteUser(int userId);
        Task<BaseResponseModel> AddFriend(AddFriendRequestModel request);
    }
}
