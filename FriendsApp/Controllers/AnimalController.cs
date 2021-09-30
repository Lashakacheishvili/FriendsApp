using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ServiceInterfaces;
using ServiceModels.Models.Animal;
using System.Threading.Tasks;

namespace FriendsApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AnimalController : BaseController
    {
        readonly IAnimalService _animalService;
        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }
        #region Gets Method
        /// <summary>
        /// Animals
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("animals")]
        [AllowAnonymous]
        public async Task<AnimalResponseModel> GetAnimals([FromQuery] AnimalRequestModel request)
        {
            return await _animalService.GetAnimals(request);
        }
        #endregion
    }
}
