using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using virtual_pet_game.Areas.v1.Controllers;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Managers.Contracts;
using virtual_pet_game.Areas.v1.Managers.Implementation;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Models.DTO;
using virtual_pet_game.Areas.v1.Repository.Contracts;
using virtual_pet_game.Tests.v1.Models.Helper;
using virtual_pet_game_Areas.v1.Repository.Contracts;
using virtual_pet_game_Areas.v1.Repository.Implementation;
using virtual_pet_game.Areas.v1.Repository.Implementation;

namespace virtual_pet_game.Tests.v1.Controllers
{
    [TestClass]
    public class AnimalControllerTests
    {

        private List<Animal> mockAnimals = new List<Animal>()
        {
             new Animal()
            {
                Id = 1,
                AnimalTypeId = 1,
                Happiness = 50,
                Hunger = 50,
                LastChecked = DateTime.Now,
                Name = "Gazza",
                UserId = 1

            },

              new Animal()
            {
                Id = 2,
                AnimalTypeId = 1,
                Happiness = 50,
                Hunger = 50,
                LastChecked = DateTime.Now,
                Name = "Charles",
                UserId = 1

            }
        };

        public List<User> mockUsers { get; set; } = new List<User>()
        {
            new User()
            {
             Id = 1,
             FirstName = "Jack",
             LastName = "Rhodes",
             Password = "fooBar"
            },

            new User()
            {

             Id = 2,
             FirstName = "Elvis",
             LastName = "Presley",
             Password = "foofooBar"
            },

        };


        IAnimalRepository animalRepository;
        IAnimalManager animalManager;
        IUserManager userManager;
        IUserRepository userRepository;
        AnimalController animalController;

        [TestInitialize]
        public void TestSetup()
        {
            HelperMethods.InitialiseAutoMapper();

            Mock<IContext> context = new Mock<IContext>();

            context.Setup(x => x.Animals).Returns(mockAnimals);
            context.Setup(x => x.Users).Returns(mockUsers);

            animalRepository = new AnimalRepository(context.Object);

            animalManager = new AnimalManager(animalRepository);

            userRepository = new UserRepository(context.Object);

            userManager = new UserManager(userRepository);

            animalController = new AnimalController(userManager,animalManager);
        }

        [TestMethod]
        public void Get_ShouldReturnIENumerableAnimalDTO_WhenValidUserId()
        {
            var result = animalController.Get(1);
            var response = result as OkObjectResult;
            var responseValue = response.Value as IEnumerable<AnimalDTO>;
            List<AnimalDTO> animals = responseValue.ToList();

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(mockAnimals.Count, animals.Count);
            Assert.AreEqual(1, animals[0].Id);
            Assert.AreEqual(2, animals[1].Id);
        }

        [TestMethod]
        public void Get_ShouldReturnEmptyIEnumerableOfAnimalDTOs_WhenNoAnimalDTOsAssociatedWithUser()
        {
            var result = animalController.Get(2);
            var response = result as OkObjectResult;
            var responseValue = response.Value as IEnumerable<AnimalDTO>;
            List<AnimalDTO> animals = responseValue.ToList();

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(0, animals.Count);
        }
        
        [TestMethod]
        public void GetById_ShouldReturnAnimalDTO_WhenValidId()
        {
            var result = animalController.GetById(1,1);
            var response = result as OkObjectResult;
            var responseValue = response.Value as AnimalDTO;
            AnimalDTO animal = responseValue;

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(1, animal.Id);
            Assert.AreEqual("Gazza", animal.Name);
            Assert.AreEqual(50, animal.Happiness);
            Assert.AreEqual(50, animal.Hunger);
        }

        [TestMethod]
        public void GetAnimalById_ShouldReturn404_WhenCalledWithInvalidAnimalId()
        {
            var result = animalController.GetById(1,100);
            var response = result as NotFoundObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.AreEqual("Animal: 100 was not found", response.Value);
        }

        [TestMethod]
        public void GetAnimalById_ShouldReturn404_WhenCalledWithInvalidUserId()
        {
            var result = animalController.GetById(100, 100);
            var response = result as NotFoundObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.AreEqual("User: 100 was not found", response.Value);
        }

    }
}
