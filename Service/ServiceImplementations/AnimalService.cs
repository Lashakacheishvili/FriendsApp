using Domain;
using Microsoft.EntityFrameworkCore;
using Service.Helper;
using Service.ServiceInterfaces;
using ServiceModels;
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
        public async Task<AnimalResponseModel> GetAnimals(AnimalRequestModel request)
        {
            var command = $@"Select ""Id"" ,""Name"" from ""Animals"" f where ""DeleteDate""  isnull  
                             {(!string.IsNullOrEmpty(request.Name) ? $@" and f.""Name"" like N'%{request.Name}%'" : string.Empty)}";
            var gen = new CRUDGenerator<AnimalRequestModel, Domain.Models.User, AnimalItemModel>(request, _context.Database.GetDbConnection(), command);
            var resp = await gen.GenerateSelectAndCount();
            return new AnimalResponseModel
            {
                Animals = resp.Item1,
                TotalCount = resp.Item2
            };
        }
        //Update Animal
        public async Task<BaseResponseModel> UpdateUser(CreateEditAnimalModel model)
        {
            var gen = new CRUDGenerator<CreateEditAnimalModel, Domain.Models.Animal, BaseResponseModel>(model, _context.Database.GetDbConnection());
            return await gen.GenerateUpdate();
        }
        //Add Animal
        public async Task<BaseResponseModel> InsertUser(CreateEditAnimalModel model)
        {
            var gen = new CRUDGenerator<CreateEditAnimalModel, Domain.Models.Animal, BaseResponseModel>(model, _context.Database.GetDbConnection());
            return await gen.GenerateInsert();
        }
        //Delete Animal
        public async Task<BaseResponseModel> DeleteUser(int id)
        {
            var gen = new CRUDGenerator<int?, Domain.Models.Animal, BaseResponseModel>(null, _context.Database.GetDbConnection());
            return await gen.GenerateSoftDelete(id);
        }
    }
}
