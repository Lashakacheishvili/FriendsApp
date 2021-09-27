using ServiceModels.Models.Animal;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces
{
    public interface IAnimalService
    {
        Task<AnimalResponseModel> GetUser(AnimalRequestModel request);
    }
}
