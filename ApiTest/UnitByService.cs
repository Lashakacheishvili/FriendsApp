using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.ServiceImplementations;
using Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest
{
    [TestClass]
    public class UnitByService
    {
        private readonly IAnimalService _animalService;
        #region Congiguration Injection
        // IConfiguration configuration=new ConfigurationBuilder().Build(); 
        #endregion
        public UnitByService()
        {
            var serviceProvider = new ServiceCollection()
        .AddDbContext<Domain.FriendsAppDbContext>(options => options.UseNpgsql("User ID=postgres;Password=A1..erti...;Host=localhost;Port=5432;Database=FriendsApp;Pooling=true;Minimum Pool Size=5;Maximum Pool Size=100;Persist Security Info=true;"))
        .BuildServiceProvider();
            var dbContext = serviceProvider.GetService<Domain.FriendsAppDbContext>();
            _animalService = new AnimalService(dbContext/*,configuration*/);
        }
        [TestMethod]
        public async Task TestMethod1()
        {
            #region Request TestCase
            var requests = new List<ServiceModels.Models.Animal.AnimalRequestModel>
            {
                new ServiceModels.Models.Animal.AnimalRequestModel{},
                new ServiceModels.Models.Animal.AnimalRequestModel
                {
                     Name ="1"
                },
                new ServiceModels.Models.Animal.AnimalRequestModel
                {
                     Name="a"
                }
            };
            #endregion
            CollectionAssert.AllItemsAreNotNull(requests);
            var response = new List<ServiceModels.Models.Animal.AnimalResponseModel>();
            Console.WriteLine("Start Test");

            foreach (var item in requests)
            {
                var resp = await _animalService.GetAnimals(item);
                if (resp != null && resp.TotalCount > 0)
                {
                    Console.WriteLine(string.Join(",", resp.Animals.Select(s => s.Name)));
                    response.Add(resp);
                }

            }
            CollectionAssert.AllItemsAreNotNull(response);
            Console.WriteLine(response.Count.ToString());
            Console.WriteLine("End Test");
        }
    }
}
