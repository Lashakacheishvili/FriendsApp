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
            Console.WriteLine("Start Test");
            var dasdad = await _animalService.GetAnimals(new ServiceModels.Models.Animal.AnimalRequestModel {});
            if (dasdad != null && dasdad.TotalCount > 0)
                Console.WriteLine(string.Join(",", dasdad.Animals.Select(s=>s.Name)));
            Console.WriteLine("End Test");
        }
    }
}
