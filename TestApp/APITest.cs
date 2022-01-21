using FriendsApi.Controllers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace TestApp
{
    public class APITest
    {
        private UserController _userController { get; set; } = null!;
        [SetUp]
        public void Setup()
        {
        }

        [TestMethod]
        public async Task Test1()
        {
            var dsada = await _userController.GetUser(new ServiceModels.Models.User.UserRequestModel { Id = 1 });
            Assert.IsNotNull(dsada);
            //Assert.Throws<InvalidOperationException>(()=> Task.Run(()=> _userController.GetUser(new ServiceModels.Models.User.UserRequestModel { Id = 1 })));
        }
        //[Test]
        //public  void Test2()
        //{
        //    var dsada = Task.Run(() => _userController.GetUser(new ServiceModels.Models.User.UserRequestModel { Id = 1 }));
        //    Assert.IsNull(dsada.Result);
        //    //Assert.Throws<InvalidOperationException>(()=> Task.Run(()=> _userController.GetUser(new ServiceModels.Models.User.UserRequestModel { Id = 1 })));
        //}
    }
}