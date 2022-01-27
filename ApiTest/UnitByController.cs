using FriendsApi.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.ServiceImplementations;
using Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTest
{
    [TestClass]
    public class UnitByController
    {
        private readonly IUserService _primeService;
        public UnitByController()
        {
            var serviceProvider = new ServiceCollection()
        .AddDbContext<Domain.FriendsAppDbContext>(options => options.UseNpgsql("User ID=postgres;Password=A1..erti...;Host=localhost;Port=5432;Database=FriendsApp;Pooling=true;Minimum Pool Size=5;Maximum Pool Size=100;Persist Security Info=true;"))
        .BuildServiceProvider();
            var dbContext = serviceProvider.GetService<Domain.FriendsAppDbContext>();
            _primeService = new UserService(dbContext);
        }
        [TestMethod]
        public async Task TestGetUser()
        {
            Console.WriteLine("Start Test");
            UserController ct = new UserController(_primeService);
            var dasdad = await ct.GetUser(new ServiceModels.Models.User.UserRequestModel { Id = 1 });
            if (dasdad != null )
                Console.WriteLine(dasdad.UserName);
            Console.WriteLine("End Test");
        }
    }
}
