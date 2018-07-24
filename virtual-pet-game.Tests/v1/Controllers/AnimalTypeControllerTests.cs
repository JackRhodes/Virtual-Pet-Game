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
    [TestClass]
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

        [TestMethod]
        public void GetAnimalTypeById_ShouldReturnUser_WhenCalledWithValidId()
        {
            var result = animalTypeController.GetById(1);
            var response = result as OkObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);

            AnimalTypeDTO animalTypeDTO = response.Value as AnimalTypeDTO;
            Assert.AreEqual(mockAnimalTypes[0].Id, animalTypeDTO.Id);
        }

        [TestMethod]
        public void GetAnimalTypeById_Should404_WhenCalledWithInvalidId()
        {
            var result = animalTypeController.GetById(100);
            var response = result as NotFoundObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.AreEqual("100 was not found", response.Value);
        }

        [TestMethod]
        public void CreateAnimalType_ShouldReturn201_WhenCalledWithValidAnimalCreationDTO()
        {
            AnimalTypeCreationDTO animalTypeCreation = new AnimalTypeCreationDTO()
            {
                AnimalTypeName = "Hamster",
                HappinessDeductionRate = 2,
                HungerIncreaseRate = 9
            };

            var result = animalTypeController.Create(animalTypeCreation);
            var response = result as CreatedAtRouteResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(201, response.StatusCode);
            Assert.AreEqual("GetAnimalTypeById", response.RouteName);
            Assert.IsTrue(response.Value is AnimalTypeCreatedDTO);

            AnimalTypeCreatedDTO animalTypeCreatedDTO = response.Value as AnimalTypeCreatedDTO;

            Assert.AreEqual(animalTypeCreation.AnimalTypeName, animalTypeCreatedDTO.AnimalTypeName);
            Assert.AreEqual(animalTypeCreation.HappinessDeductionRate, animalTypeCreatedDTO.HappinessDeductionRate);
            Assert.AreEqual(animalTypeCreation.HungerIncreaseRate, animalTypeCreatedDTO.HungerIncreaseRate);
        }

        [TestMethod]
        public void DeleteAnimalType_ShouldReturn204_WhenValidId()
        {
            var result = animalTypeController.Delete(1);
            var response = result as NoContentResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(204, response.StatusCode);
        }


        [TestMethod]
        public void DeleteAnimalType_ShouldReturnBadRequest_WhenInvalidId()
        {
            var result = animalTypeController.Delete(1234234234);
            var response = result as BadRequestResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(400, response.StatusCode);
        }

        [TestMethod]
        public void UpdateAnimalType_ShouldReturn204_WhenValidId()
        {
            AnimalTypeCreationDTO animalType = new AnimalTypeCreationDTO()
            {
                AnimalTypeName = "Lovely Doggo",
                HappinessDeductionRate = 9,
                HungerIncreaseRate = 3
            };
            
            var result = animalTypeController.Update(1, animalType);
            var response = result as NoContentResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(204, response.StatusCode);

            var getResult = animalTypeController.GetById(1);
            var getResponse = getResult as OkObjectResult;

            var getValue = getResponse.Value as AnimalTypeDTO;

            Assert.AreEqual(animalType.AnimalTypeName, getValue.AnimalTypeName);
        }

        [TestMethod]
        public void UpdatedAnimalType_ShouldReturn404_WhenCalledWithInvalidId()
        {
            var result = animalTypeController.GetById(100);
            var response = result as NotFoundObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.AreEqual("100 was not found", response.Value);
        }

    }
}
