using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Repository.Contracts;
using virtual_pet_game.Areas.v1.Repository.Implementation;

namespace virtual_pet_game.Tests.v1.Repository
{
    [TestClass]
    public class AnimalRepositoryTests
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

        [TestInitialize]
        public void TestSetup()
        {
            Mock<IContext> context = new Mock<IContext>();

            context.Setup(x => x.Animals).Returns(mockAnimals);

            animalRepository = new AnimalRepository(context.Object);
        }

        [TestMethod]
        public void GetAnimalsFromUsers_ShouldReturn_IENumerableOfAnimals_WhenValidId()
        {
            List<Animal> returnAnimals =  animalRepository.GetAnimalsFromUser(1).ToList();

            Assert.AreEqual(2, returnAnimals.Count);
            Assert.AreEqual(1, returnAnimals[0].Id);
            Assert.AreEqual(2, returnAnimals[1].Id);
        }

        [TestMethod]
        public void GetAnimalsFromUser_ShoudReturnNull_WhenNoAnimals()
        {
            //It is valid here to have 0 animals. Therefore I will return an empty list rather than throwing any exceptions

            IEnumerable<Animal> returnAnimals = animalRepository.GetAnimalsFromUser(2);
            Assert.AreEqual(0, returnAnimals.Count());
        }

        [TestMethod]
        public void GetAnimalById_ShouldReturnAnimalTypes_WhenValidId()
        {
            Animal animal = animalRepository.GetAnimalById(1);
            Assert.AreEqual(mockAnimals[0].Id, animal.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetAnimalById_ShouldThrowExcpetion_WhenInvalidId()
        {
            //Handled by controller
            Animal animal = animalRepository.GetAnimalById(100);
        }


        [TestMethod]
        public void CreateAnimal_ShouldCreateAnimal_WhenValid()
        {
            Animal animal = new Animal()
            {
                Id = 3,
                AnimalTypeId = 1,
                Happiness = 50,
                Hunger = 50,
                LastChecked = DateTime.Now,
                Name = "Antonio",
                UserId = 2
            };

            int animalCount = animalRepository.GetAnimalsFromUser(2).Count();

            Animal returnValue = animalRepository.CreateAnimal(animal);

            Assert.AreEqual(0, animalCount);
            animalCount = animalRepository.GetAnimalsFromUser(2).Count();
            Assert.AreEqual(1, animalCount);
            Assert.AreEqual(animal.Id, returnValue.Id);
            Assert.AreEqual(animal.AnimalTypeId, returnValue.AnimalTypeId);
            Assert.AreEqual(animal.Happiness, returnValue.Happiness);
            Assert.AreEqual(animal.Hunger, returnValue.Hunger);
            Assert.AreEqual(animal.UserId, returnValue.UserId);
            Assert.AreEqual(animal.LastChecked, returnValue.LastChecked);
        }

        [TestMethod]
        public void DeleteAnimal_ShouldRemoveAnimal_WhenValid()
        {
            Animal animal = animalRepository.GetAnimalById(1);

            int animalCount = mockAnimals.Count;

            animalRepository.DeleteAnimal(animal);

            Assert.AreEqual(animalCount - 1, mockAnimals.Count);
            Assert.ThrowsException<InvalidOperationException>(() => animalRepository.GetAnimalById(1));
        }

    }
}
