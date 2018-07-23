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
    public class AnimalTypeRepositoryTests
    {
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

        private IAnimalTypeRepository animalTypeRepository;

        [TestInitialize]
        public void TestSetup()
        {
            Mock<IContext> context = new Mock<IContext>();

            context.Setup(x => x.AnimalTypes).Returns(mockAnimalTypes);

            animalTypeRepository = new AnimalTypeRepository(context.Object);

        }

        [TestMethod]
        public void GetAnimals_ShouldReturnAnimals_WhenNoId()
        {
            List<AnimalType> animalTypes = animalTypeRepository.GetAnimalTypes().ToList();

            Assert.AreEqual(mockAnimalTypes.Count, animalTypes.Count);
            Assert.IsTrue(mockAnimalTypes.SequenceEqual(animalTypes));
        }

        [TestMethod]
        public void GetAnimalTypesById_ShouldReturnAnimalTypes_WhenValidId()
        {
            AnimalType animalType = animalTypeRepository.GetAnimalTypeById(1);
            Assert.ReferenceEquals(mockAnimalTypes[0], animalType);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetAnimalById_ShouldThrowExcpetion_WhenInvalidId()
        {
            //Handled by controller
            AnimalType animalType = animalTypeRepository.GetAnimalTypeById(100);
        }


        [TestMethod]
        public void AddAnimalType_ShouldAddAnimalType_WhenValid()
        {
            AnimalType animalType = new AnimalType()
            {
                Id = 3,
                AnimalTypeName = "Donkey",
                HappinessDeductionRate = 5,
                HungerIncreaseRate = 3
            };

            int animalCount = animalTypeRepository.GetAnimalTypes().Count();

            AnimalType returnValue = animalTypeRepository.CreateAnimalType(animalType);

            Assert.AreEqual(2, animalCount); 
            animalCount = animalTypeRepository.GetAnimalTypes().Count();
            Assert.AreEqual(3, animalCount);
            Assert.AreEqual(animalType.Id, returnValue.Id);
            Assert.AreEqual(animalType.AnimalTypeName, returnValue.AnimalTypeName);
            Assert.AreEqual(animalType.HappinessDeductionRate, returnValue.HappinessDeductionRate);
            Assert.AreEqual(animalType.HungerIncreaseRate, returnValue.HungerIncreaseRate);
        }

        [TestMethod]
        public void DeleteAnimalType_ShouldRemoveAnimal_WhenValid()
        {
            Assert.AreEqual(2, mockAnimalTypes.Count);
            AnimalType animalTypeToRemove = animalTypeRepository.GetAnimalTypeById(1);
            animalTypeRepository.DeleteAnimalType(animalTypeToRemove);
            Assert.AreEqual(1, animalTypeRepository.GetAnimalTypes().Count());
            
        }

    }
}
