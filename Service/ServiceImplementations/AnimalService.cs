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
        #region Get
        //Animals
        public async Task<AnimalResponseModel> GetAnimals(AnimalRequestModel request)
        {
            var gen = new CRUDGenerator<AnimalRequestModel, Domain.Models.Animal, AnimalItemModel>(request, _context.Database.GetDbConnection(), string.Empty);
            var resp = await gen.GenerateSelectAndCount(generationWhere: true);
            return new AnimalResponseModel
            {
                Animals = resp.List,
                TotalCount = resp.TotalCount
            };
        }
        #endregion
        #region Update
        //Update Animal
        public async Task<BaseResponseModel> UpdateUser(CreateEditAnimalModel model)
        {
            var gen = new CRUDGenerator<CreateEditAnimalModel, Domain.Models.Animal, BaseResponseModel>(model, _context.Database.GetDbConnection());
            return await gen.GenerateUpdate();
        }
        #endregion
        #region Create Or Insert
        //Add Animal
        public async Task<BaseResponseModel> InsertUser(CreateEditAnimalModel model)
        {
            var gen = new CRUDGenerator<CreateEditAnimalModel, Domain.Models.Animal, BaseResponseModel>(model, _context.Database.GetDbConnection());
            return await gen.GenerateInsert();
        }
        #endregion
        #region Delete
        //Delete Animal
        public async Task<BaseResponseModel> DeleteUser(int id)
        {
            var gen = new CRUDGenerator<int?, Domain.Models.Animal, BaseResponseModel>(null, _context.Database.GetDbConnection());
            return await gen.GenerateSoftDelete(id);
        }
        #endregion
    }
}
