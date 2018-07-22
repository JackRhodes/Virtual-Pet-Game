using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Managers.Implementation;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Models.DTO;
using virtual_pet_game.Areas.v1.Models.Mappings;
using virtual_pet_game_Areas.v1.Repository.Contracts;
using virtual_pet_game_Areas.v1.Repository.Implementation;

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

        List<UserDTO> expectedResults;

        IUserRepository userRepository;

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

            context.Setup(x => x.Users).Returns(mockUser);
            //No need to Mock the actual repository
            userRepository = new UserRepository(context.Object);

            userManager = new UserManager(userRepository);

            expectedResults = new List<UserDTO>();

            foreach (var user in mockUser)
            {
                expectedResults.Add(

                    new UserDTO()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    }

                    );
            }


        }

        [TestMethod]
        public void GetUsers_ShouldReturnUsersAsDTO_WhenRan()
        {
            List<UserDTO> result = userManager.GetUsers().ToList();

            Assert.AreEqual(expectedResults.Count, result.Count);
            Assert.AreEqual(expectedResults[0].FirstName, result[0].FirstName);
            Assert.AreEqual(expectedResults[0].LastName, result[0].LastName);
            Assert.AreEqual(expectedResults[1].FirstName, result[1].FirstName);
            Assert.AreEqual(expectedResults[1].LastName, result[1].LastName);
        }

        [TestMethod]
        public void GetUserById_ShouldReturnUser_WhenValidId()
        {
            UserDTO result = userManager.GetUserById(1);
            Assert.AreEqual(expectedResults[0].FirstName, result.FirstName);
            Assert.AreEqual(expectedResults[0].LastName, result.LastName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetUserById_ShouldThrowException_WhenInvalidId()
        {
            UserDTO result = userManager.GetUserById(100);
        }

        [TestMethod]
        public void AddUser_ShouldAddUserToDatasource_WhenValidUserCreationDTO()
        {
            UserCreationDTO userCreation = new UserCreationDTO()
            {
                FirstName = "Gareth",
                LastName = "SouthGate",
                Password = "It is not coming home"
            };

            var returnValue = userManager.AddUser(userCreation);

            Assert.AreEqual(userCreation.FirstName, returnValue.FirstName);
            Assert.AreEqual(userCreation.LastName, returnValue.LastName);
            Assert.AreEqual(3, returnValue.Id);

        }

    }
}
