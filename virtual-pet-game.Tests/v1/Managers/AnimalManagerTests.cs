﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

    }
}
