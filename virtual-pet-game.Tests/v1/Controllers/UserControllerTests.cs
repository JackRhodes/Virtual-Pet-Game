using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using virtual_pet_game.Areas.v1.Controllers;
using virtual_pet_game.Areas.v1.Models.DTO;

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

            List<UserDTO> userDTOs = response.Value as List<UserDTO>;

            Assert.AreEqual("Jack", userDTOs[0].FirstName);
            Assert.AreEqual("Rhodes", userDTOs[0].LastName);
            Assert.AreEqual("Elvis", userDTOs[1].FirstName);
            Assert.AreEqual("Presley", userDTOs[1].LastName);
        }
    }
}
