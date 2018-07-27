﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Repository.Contracts;

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
    }
}