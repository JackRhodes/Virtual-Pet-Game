using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Exceptions;
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

            },

              new Animal()
            {
                Id = 3,
                AnimalTypeId = 1,
                Happiness = 100,
                Hunger = 0,
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

        public List<AnimalType> mockAnimalTypes = new List<AnimalType>()
        {
            new AnimalType()
            {
                Id = 1,
                AnimalTypeName = "Doggo",
                HappinessDeductionRate = 2,
                HungerIncreaseRate = 3

            },
            new AnimalType()
            {
                Id = 2,
                AnimalTypeName = "Cat",
                HappinessDeductionRate = 4,
                HungerIncreaseRate = 2
            }

        };

        IAnimalRepository animalRepository;
        IAnimalManager animalManager;
        IAnimalTypeManager animalTypeManager;
        IAnimalTypeRepository animalTypeRepository;
        IAnimalStateManager animalStateManager;

        [TestInitialize]
        public void TestSetup()
        {
            //This is redundant if a previous test executes first. But should be here for safe programming.
            HelperMethods.InitialiseAutoMapper();

            Mock<IContext> context = new Mock<IContext>();

            context.Setup(x => x.Animals).Returns(mockAnimals);
            context.Setup(x => x.AnimalTypes).Returns(mockAnimalTypes);
            context.Setup(x => x.Users).Returns(mockUsers);

            animalRepository = new AnimalRepository(context.Object);
            animalTypeRepository = new AnimalTypeRepository(context.Object);
            animalTypeManager = new AnimalTypeManager(animalTypeRepository);
            animalStateManager = new AnimalStateManager();
            animalManager = new AnimalManager(animalRepository, animalTypeManager, animalStateManager);
        }

        [TestMethod]
        public void GetAnimalsByUserId_ShouldReturnAnimalDTOs_WhenValidUserId()
        {
            int animalCount = mockAnimals.Count;

            List<AnimalDTO> animalDTOs = animalManager.GetAnimalsByUserId(1).ToList();

            Assert.AreEqual(animalCount, animalDTOs.Count);
            Assert.AreEqual(1, animalDTOs[0].Id);
            Assert.AreEqual(2, animalDTOs[1].Id);

            //Check mapping is correct
            Assert.AreEqual(50, animalDTOs[0].Hunger);
            Assert.AreEqual(50, animalDTOs[0].Happiness);
            Assert.AreEqual("Gazza", animalDTOs[0].Name);
        }

        [TestMethod]
        public void GetAnimalsByUserId_ShouldReturnEmptyIEnumerableOfAnimalDTOs_WhenValidUserIdAndNoAnimals()
        {
            List<AnimalDTO> animalDTOs = animalManager.GetAnimalsByUserId(2).ToList();

            Assert.AreEqual(0, animalDTOs.Count);
        }

        [TestMethod]
        public void GetAnimalsById_ShouldReturnAnimalDTO_WhenValidId()
        {
            AnimalDTO animalDTO = animalManager.GetAnimalById(1, 1);

            Assert.AreEqual(1, animalDTO.Id);
            Assert.AreEqual("Gazza", animalDTO.Name);
            Assert.AreEqual(50, animalDTO.Happiness);
            Assert.AreEqual(50, animalDTO.Hunger);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetAnimalsById_ShouldThrowInvalidOperationException_WhenInvalidId()
        {
            animalManager.GetAnimalById(1, 123123);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotOwnedException))]
        public void GetAnimalById_ShouldThrowResourceNotOwnedException_WhenUserIdDoesNotOwnChildResource()
        {
            animalManager.GetAnimalById(2, 1);
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
        

        [TestMethod]
        public void FeedAnimal_ShouldDecreaseHunger_WhenFed()
        {
            AnimalDTO animal = animalManager.FeedAnimal(1, 1);

            Assert.AreEqual(30, animal.Hunger);
        }

        [TestMethod]
        public void FeedAnimal_ShouldBeMinimumHunger_WhenFedWhenFull()
        {
            AnimalDTO animal = animalManager.FeedAnimal(1, 3);

            Assert.AreEqual(0, animal.Hunger);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotOwnedException))]
        public void FeedAnimal_ShouldThrowResourceNotOwnedException_WhenUserIdDoesNotOwnChildResource()
        {
            animalManager.FeedAnimal(2, 1);
        }
        
        [TestMethod]
        public void PetAnimal_ShouldIncreaseHappiness_WhenPet()
        {
            AnimalDTO animal = animalManager.PetAnimal(1, 1);

            Assert.AreEqual(70, animal.Happiness);
        }

        [TestMethod]
        public void PetAnimal_ShouldBeMaximumHappiness_WhenPetWhenFull()
        {
            AnimalDTO animal = animalManager.PetAnimal(1, 3);

            Assert.AreEqual(100, animal.Happiness);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotOwnedException))]
        public void PetAnimal_ShouldThrowResourceNotOwnedException_WhenUserIdDoesNotOwnChildResource()
        {
            animalManager.PetAnimal(2, 1);
        }
    }
}
