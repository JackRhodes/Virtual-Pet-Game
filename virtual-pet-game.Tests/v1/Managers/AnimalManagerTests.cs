using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Managers.Contracts;
using virtual_pet_game.Areas.v1.Managers.Implementation;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Models.DTO;
using virtual_pet_game.Areas.v1.Repository.Contracts;
using virtual_pet_game.Areas.v1.Repository.Implementation;
using virtual_pet_game.Tests.v1.Models.Helper;

namespace virtual_pet_game.Tests.v1.Managers
{
    [TestClass]
    public class AnimalManagerTests
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

        [TestInitialize]
        public void TestSetup()
        {
            //This is redundant if a previous test executes first. But should be here for safe programming.
            HelperMethods.InitialiseAutoMapper();

            Mock<IContext> context = new Mock<IContext>();

            context.Setup(x => x.Animals).Returns(mockAnimals);

            animalRepository = new AnimalRepository(context.Object);

            animalManager = new AnimalManager(animalRepository);
        }

        [TestMethod]
        public void GetAnimalsByUserId_ShouldReturnAnimalDTOs_WhenValidUserId ()
        {
           List<AnimalDTO> animalDTOs = animalManager.GetAnimalsByUserId(1).ToList();

            Assert.AreEqual(2, animalDTOs.Count);
            Assert.AreEqual(1, animalDTOs[0].Id);
            Assert.AreEqual(2, animalDTOs[1].Id);

            //Check mapping is correct
            Assert.AreEqual(50, animalDTOs[0].Hunger);
            Assert.AreEqual(50, animalDTOs[0].Happiness);
            Assert.AreEqual("Gazza", animalDTOs[0].Name);
        }

        [TestMethod]
        public void GetAnimalsByUserId_ShouldReturnEmptyIEnumerableOfAnimalDTOs_WhenInvalidUserId()
        {
            List<AnimalDTO> animalDTOs = animalManager.GetAnimalsByUserId(2).ToList();

            Assert.AreEqual(0, animalDTOs.Count);
        }

        [TestMethod]
        public void GetAnimalsById_ShouldReturnAnimalDTO_WhenValidId()
        {
            AnimalDTO animalDTO = animalManager.GetAnimalById(1);

            Assert.AreEqual(1, animalDTO.Id);
            Assert.AreEqual("Gazza", animalDTO.Name);
            Assert.AreEqual(50, animalDTO.Happiness);
            Assert.AreEqual(50, animalDTO.Hunger);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetAnimalsById_ShouldThrowInvalidOperationException_WhenInvalidId()
        {
            animalManager.GetAnimalById(123123);
        }

        [TestMethod]
        public void CreateAnimal_ShouldCreateAnimal_WhenValidDTO()
        {
            AnimalCreationDTO animalCreationDTO = new AnimalCreationDTO()
            {
                Name = "Gareth",
                AnimalTypeId = 1
            };

            AnimalDTO animalDTO = animalManager.CreateAnimal(1, animalCreationDTO);

            //Check if the model is valid
            Assert.IsTrue(HelperMethods.CheckModelValidation(animalDTO));
            Assert.AreEqual(animalCreationDTO.Name, animalDTO.Name);
            Assert.AreEqual(animalCreationDTO.AnimalTypeId, animalDTO.AnimalTypeId);
            Assert.AreEqual(mockAnimals.Count, animalDTO.Id);

            //Should be default Value
            Assert.AreEqual(HelperMethods.DEFAULT_HAPPINESS, animalDTO.Happiness);
            Assert.AreEqual(HelperMethods.DEFAULT_HUNGER, animalDTO.Hunger);

        }

        [TestMethod]
        public void DeleteAnimal_ShouldRemoveAnimal_WhenValid()
        {
            int animalCount = mockAnimals.Count;
            animalManager.DeleteAnimal(1);

            Assert.AreEqual(animalCount - 1, mockAnimals.Count);
            Assert.ThrowsException<InvalidOperationException>(() => animalRepository.GetAnimalById(1));
        }
    }
}
