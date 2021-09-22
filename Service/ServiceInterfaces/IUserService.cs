using ServiceModels.Models.User;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserItemModel> GetUser(UserRequestModel request);
    }
}
