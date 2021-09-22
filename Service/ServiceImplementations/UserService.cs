using Domain;
using Microsoft.EntityFrameworkCore;
using Service.Helper;
using Service.ServiceInterfaces;
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
        public async Task<UserItemModel> GetUser(UserRequestModel request)
        {
            var command = $@"Select ""Id"" ,""UserName"",""SiteUrl"" URL from ""Users"" u where u.""Id""={request.Id}";
            var gen = new CRUDGenerator<UserRequestModel, Domain.Models.User, UserItemModel>(request, _context.Database.GetDbConnection(), command);
            var resp= await gen.GenerateSelectAndCount();
            return resp?.FirstOrDefault();
        }
    }
}
