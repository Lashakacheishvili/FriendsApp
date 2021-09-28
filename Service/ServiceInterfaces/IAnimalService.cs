using ServiceModels;
using ServiceModels.Models.Animal;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces
{
    public interface IAnimalService
    {
        Task<AnimalResponseModel> GetAnimals(AnimalRequestModel request);
        Task<BaseResponseModel> UpdateUser(CreateEditAnimalModel model);
        Task<BaseResponseModel> InsertUser(CreateEditAnimalModel model);
        Task<BaseResponseModel> DeleteUser(int id);
    }
}
