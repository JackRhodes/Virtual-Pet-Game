using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using virtual_pet_game.Areas.v1.Controllers;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Managers.Contracts;
using virtual_pet_game.Areas.v1.Managers.Implementation;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Models.DTO;
using virtual_pet_game.Areas.v1.Models.Mappings;
using virtual_pet_game.Areas.v1.Repository.Contracts;
using virtual_pet_game.Areas.v1.Repository.Implementation;

namespace virtual_pet_game.Tests.v1.Controllers
{
    public class AnimalTypeControllerTests
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
        private AnimalTypeController animalTypeController;

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

            animalTypeController = new AnimalTypeController(animalTypeManager);

        }

        [TestMethod]
        public void Get_ShouldReturnAllAnimalTypes()
        {
            var result = animalTypeController.Get();
            var response = result as OkObjectResult;
            var animalTypes = response.Value as IEnumerable<AnimalTypeDTO>;

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.AreEqual(mockAnimalTypes.Count, animalTypes.Count());
            Assert.AreEqual(mockAnimalTypes[0].Id, animalTypes.ToList()[0].Id);
        }

    }
}
