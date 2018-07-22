using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game_Areas.v1.Repository.Implementation;

namespace virtual_pet_game.Tests.v1.Repository
{
    [TestClass]
    public class UserRepositoryTests
    {
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

        UserRepository userRepository;

        [TestInitialize]
        public void TestSetup()
        {
            Mock<IContext> context = new Mock<IContext>();

            context.Setup(x => x.Users).Returns(mockUser);

            userRepository = new UserRepository(context.Object);

        }

        [TestMethod]
        public void GetUsers_ShouldReturnIEnumerableOfUsers_WhenCalled()
        {
            List<User> results = userRepository.GetUsers().ToList();

            Assert.AreEqual(mockUser.Count, results.Count);
            Assert.AreEqual(true, mockUser.SequenceEqual(results));
        }

        [TestMethod]
        public void GetUserById_ShouldReturnUsers_WhenValidId()
        {
            User user = userRepository.GetUserById(1);
            Assert.ReferenceEquals(mockUser[0], user);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetUserById_ShouldThrowExcpetion_WhenInvalidId()
        {
            //Handled by controller
            User user = userRepository.GetUserById(100);
        }

        [TestMethod]
        public void AddUser_ShouldAddUser_WhenValid()
        {
            User user = new User()
            {
                Id = 3,
                FirstName = "Bob",
                LastName = "Marley",
                Password = "Do Not Worry"
            };
            
            int userCount = userRepository.GetUsers().Count();

            User returnValue = userRepository.CreateUser(user);

            Assert.AreEqual(2, userCount);;
            userCount = userRepository.GetUsers().Count();
            Assert.AreEqual(3, userCount);
            Assert.AreEqual(user.FirstName, returnValue.FirstName);
            Assert.AreEqual(user.LastName, returnValue.LastName);
            Assert.AreEqual(3, user.Id);
        }

    }
}
