using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using virtual_pet_game.Areas.v1.Managers.Implementation;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Models.DTO;
using virtual_pet_game.Areas.v1.Models.Mappings;
using virtual_pet_game_Areas.v1.Repository.Contracts;

namespace virtual_pet_game.Tests.v1.Managers
{
    [TestClass]
    public class UserManagerTests
    {
        UserManager userManager;

        private readonly List<User> mockUser = new List<User>()
        {
            new User()
            {
                Id = 1,
                FirstName = "Jack",
                LastName = "Rhodes",
                Password = "Foo Foo Foo"
            },

            new User()
            {
                Id = 2,
                FirstName = "Kazuma",
                LastName = "Satou",
                Password = "Steal"
            }
        };

        [TestInitialize]
        public void TestSetup()
        {
            //As automapper is static, it can be initalised in here to replicate the functionality offered by Startup.cs
            Mapper.Initialize(x =>
            {
                x.AddProfile(new DTOMappings());
            });


            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x.GetUsers()).Returns(mockUser);

            userManager = new UserManager(mockUserRepository.Object);

        }

        [TestMethod]
        public void GetUsers_ShouldReturnUsersAsDTO_WhenRan()
        {
            List<UserDTO> expectedResults = new List<UserDTO>();

            foreach(var user in mockUser)
            {
                expectedResults.Add(

                    new UserDTO()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    }

                    );
            }


            List<UserDTO> result = userManager.GetUsers().ToList();

            Assert.AreEqual(expectedResults.Count, result.Count);
            Assert.AreEqual(expectedResults[0].FirstName, result[0].FirstName);
            Assert.AreEqual(expectedResults[0].LastName, result[0].LastName);
            Assert.AreEqual(expectedResults[1].FirstName, result[1].FirstName);
            Assert.AreEqual(expectedResults[1].LastName, result[1].LastName);
        }

    }
}
