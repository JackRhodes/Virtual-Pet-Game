using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using virtual_pet_game.Areas.v1.Controllers;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Managers.Contracts;
using virtual_pet_game.Areas.v1.Managers.Implementation;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Models.DTO;
using virtual_pet_game.Tests.v1.Models.Helper;
using virtual_pet_game_Areas.v1.Repository.Contracts;
using virtual_pet_game_Areas.v1.Repository.Implementation;

namespace virtual_pet_game.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController userController;

        private readonly IEnumerable<UserDTO> mockUsers = new List<UserDTO>()
        {
            new UserDTO()
            {
                FirstName = "Jack",
                LastName = "Rhodes"
            },
            new UserDTO()
            {
                FirstName = "Elvis",
                LastName = "Presley"
            }
        };

        private readonly List<User> mockUser = new List<User>()
        {
            new User()
            {
                Id = 1,
                FirstName = "Jack",
                LastName = "Rhodes",
                Password = "Foo Foo Foo"
            },

            new User()
            {
                Id = 2,
                FirstName = "Elvis",
                LastName = "Presley",
                Password = "Steal"
            }
        };

        IUserRepository userRepository;
        IUserManager userManager;

        [TestInitialize]
        public void TestSetup()
        {
            HelperMethods.InitialiseAutoMapper();

            Mock<IContext> context = new Mock<IContext>();

            context.Setup(x => x.Users).Returns(mockUser);
            //No need to Mock the actual repository
            userRepository = new UserRepository(context.Object);

            userManager = new UserManager(userRepository);

            userController = new UserController(userManager);
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

        [TestMethod]
        public void GetUserById_ShouldReturnUser_WhenCalledWithValidId()
        {
            var result = userController.GetById(1);
            var response = result as OkObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);

            UserDTO userDTO = response.Value as UserDTO;
            Assert.AreEqual("Jack", userDTO.FirstName);
            Assert.AreEqual("Rhodes", userDTO.LastName);
        }

        [TestMethod]
        public void GetUserById_Should404_WhenCalledWithInvalidId()
        {
            var result = userController.GetById(100);
            var response = result as NotFoundObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.AreEqual("100 was not found", response.Value);
        }

        [TestMethod]
        public void AddUser_ShouldReturn201_WhenCalledWithValidUserCreationDTO()
        {
            UserCreationDTO userCreationDTO = new UserCreationDTO()
            {
                FirstName = "Matthew",
                LastName = "Roberts",
                Password = "MattyRob"
            };

            var result = userController.AddUser(userCreationDTO);
            var response = result as CreatedAtRouteResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(201, response.StatusCode);
            Assert.AreEqual("GetUserById", response.RouteName);
            Assert.IsTrue(response.Value is UserCreatedDTO);
            UserCreatedDTO userCreatedDTO = response.Value as UserCreatedDTO;
            Assert.AreEqual(userCreationDTO.FirstName, userCreatedDTO.FirstName);
            Assert.AreEqual(userCreationDTO.LastName, userCreatedDTO.LastName);
        }

    }
}
