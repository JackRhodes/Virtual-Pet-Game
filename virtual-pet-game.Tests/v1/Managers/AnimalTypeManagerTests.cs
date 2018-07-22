using AutoMapper;
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
using virtual_pet_game.Areas.v1.Models.Mappings;
using virtual_pet_game.Areas.v1.Repository.Contracts;
using virtual_pet_game.Areas.v1.Repository.Implementation;

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
            Mapper.Reset();

            //As automapper is static, it can be initalised in here to replicate the functionality offered by Startup.cs
            Mapper.Initialize(x =>
            {
                x.AddProfile(new DTOMappings());
            });


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
        
    }
}
