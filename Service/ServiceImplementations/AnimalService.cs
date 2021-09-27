using Domain;
using Microsoft.EntityFrameworkCore;
using Service.Helper;
using Service.ServiceInterfaces;
using ServiceModels.Models.Animal;
using System.Linq;
using System.Threading.Tasks;

namespace Service.ServiceImplementations
{
    public class AnimalService : IAnimalService
    {
        private readonly FriendsAppDbContext _context;
        public AnimalService(FriendsAppDbContext context)
        {
            _context = context;
        }
        //Animals
        public async Task<AnimalResponseModel> GetUser(AnimalRequestModel request)
        {
            var command = $@"Select ""Id"" ,""UserName"",""SiteUrl"" from ""Users"" u where ""DeleteDate""  isnull  
                             {(!string.IsNullOrEmpty(request.Name) ? $@" and u.""UserName"" like N'%{request.Name}%'" : string.Empty)}";
            var gen = new CRUDGenerator<AnimalRequestModel, Domain.Models.User, AnimalItemModel>(request, _context.Database.GetDbConnection(), command);
            var resp = await gen.GenerateSelectAndCount();
            return new AnimalResponseModel
            {
                Animals = resp,
                TotalCount = resp.Count()
            };
        }
    }
}
