﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AnimalTypeManagerTests
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
        private IAnimalTypeManager animalTypeManager;

        [TestInitialize]
        public void TestSetup()
        {
            HelperMethods.InitialiseAutoMapper();
            
            Mock<IContext> context = new Mock<IContext>();

            context.Setup(x => x.AnimalTypes).Returns(mockAnimalTypes);

            animalTypeRepository = new AnimalTypeRepository(context.Object);

            animalTypeManager = new AnimalTypeManager(animalTypeRepository);

        }

        [TestMethod]
        public void GetAnimalTypes_ShouldReturnAllAnimals()
        {
            List<AnimalTypeDTO> animalTypeDTOs =  animalTypeManager.GetAnimalTypes().ToList();

            Assert.AreEqual(mockAnimalTypes.Count, animalTypeDTOs.Count);
            Assert.AreEqual(mockAnimalTypes[0].Id, animalTypeDTOs[0].Id);
            Assert.AreEqual(mockAnimalTypes[1].Id, animalTypeDTOs[1].Id);
        }

        [TestMethod]
        public void GetAnimalTypeById_ShouldReturnAnimal_WhenValidId()
        {
            AnimalTypeDTO result = animalTypeManager.GetAnimalTypeById(1);
            Assert.AreEqual(mockAnimalTypes[0].Id, result.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetUserById_ShouldThrowException_WhenInvalidId()
        {
            AnimalTypeDTO result = animalTypeManager.GetAnimalTypeById(100);
        }

        [TestMethod]
        public void AddAnimalType_ShouldAddAnimalTypeToDatasource_WhenValidAnimalTypeCreationDTO()
        {
            AnimalTypeCreationDTO animalTypeCreation = new AnimalTypeCreationDTO()
            {
                AnimalTypeName = "Crocodile",
                HappinessDeductionRate = 2,
                HungerIncreaseRate = 4
            };

            var returnValue = animalTypeManager.CreateAnimalType(animalTypeCreation);

            Assert.AreEqual(animalTypeCreation.AnimalTypeName, returnValue.AnimalTypeName);
            Assert.AreEqual(animalTypeCreation.HappinessDeductionRate, returnValue.HappinessDeductionRate);
            Assert.AreEqual(animalTypeCreation.HungerIncreaseRate, returnValue.HungerIncreaseRate);
            Assert.AreEqual(3, returnValue.Id);
        }

        [TestMethod]
        public void DeleteAnimalType_ShouldRemoveAnimalType_WhenValid()
        {
            Assert.AreEqual(2, mockAnimalTypes.Count);
            animalTypeManager.DeleteAnimalType(1);
            Assert.AreEqual(1, animalTypeManager.GetAnimalTypes().Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteAnimalType_ShouldThrowInvalidOperationException_WhenInvalidId()
        {
            animalTypeManager.DeleteAnimalType(124234);
        }

        [TestMethod]
        public void UpdateAnimalType_ShouldUpdateAnimalType_WhenValid()
        {
            AnimalTypeCreationDTO animalTypeCreationDTO = new AnimalTypeCreationDTO()
            {
                AnimalTypeName = "Good Pupper",
                HappinessDeductionRate = 4,
                HungerIncreaseRate = 5
            };


            AnimalTypeCreatedDTO updatedAnimalType = animalTypeManager.UpdateAnimalType(1, animalTypeCreationDTO);

            Assert.AreEqual(animalTypeCreationDTO.AnimalTypeName, updatedAnimalType.AnimalTypeName);
            Assert.AreEqual(animalTypeCreationDTO.HappinessDeductionRate, updatedAnimalType.HappinessDeductionRate);
            Assert.AreEqual(animalTypeCreationDTO.HungerIncreaseRate, updatedAnimalType.HungerIncreaseRate);

            AnimalTypeDTO animalTypeDTO = animalTypeManager.GetAnimalTypeById(1);

            Assert.AreEqual("Good Pupper", animalTypeDTO.AnimalTypeName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateAnimalType_ShouldReturnExcpetion_WhenInvalidId()
        {
            AnimalTypeCreationDTO animalTypeCreationDTO = new AnimalTypeCreationDTO()
            {
                AnimalTypeName = "Good Pupper",
                HappinessDeductionRate = 4,
                HungerIncreaseRate = 5
            };

            AnimalTypeCreatedDTO updatedAnimalType = animalTypeManager.UpdateAnimalType(34534, animalTypeCreationDTO);
        }

        [TestMethod]
        public void GetFullAnimalTypeById_ShouldReturnFullAnimalType_WhenValidId ()
        {
            AnimalTypeCreationDTO result = animalTypeManager.GetFullAnimalTypeById(1);
            Assert.AreEqual(mockAnimalTypes[0].AnimalTypeName, result.AnimalTypeName);
            Assert.AreEqual(mockAnimalTypes[0].HappinessDeductionRate, result.HappinessDeductionRate);
            Assert.AreEqual(mockAnimalTypes[0].HungerIncreaseRate, result.HungerIncreaseRate);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetFullAnimalTypeById_ShouldThrowException_WhenInvalidId()
        {
            AnimalTypeCreationDTO result = animalTypeManager.GetFullAnimalTypeById(100);
        }

    }
}
