using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using virtual_pet_game.Areas.v1.Controllers;

namespace virtual_pet_game.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController userController;

        [TestInitialize]
        public void TestSetup()
        {
            userController = new UserController();
        }

        [TestMethod]
        public void Get_ShouldReturnString_WhenCalledWithNoId()
        {
            var result = userController.Get();
            var response = result as OkObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual("You have reached the UserController", response.Value);
        }
    }
}
