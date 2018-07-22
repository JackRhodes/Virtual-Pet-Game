using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using virtual_pet_game.Areas.v1.Models.DTO;
using virtual_pet_game.Tests.v1.Models.Helper;

namespace virtual_pet_game.Tests.v1.Models
{
    [TestClass]
    public class UserDTOTests
    {
        HelperMethods helper;

        [TestInitialize]
        public void TestSetup()
        {
            helper = new HelperMethods();
        }
        [TestMethod]
        public void UserDTO_ShouldFailModelValidation_WhenRequiredFieldNotEntered()
        {
            UserDTO userDTO = new UserDTO()
            {
                FirstName = "Bob"
            };

            bool result = helper.CheckModelValidation(userDTO);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void UserDTO_ShouldFailModelValidation_WhenFieldsOverMaxLength()
        {

            string largeString = helper.GenerateLargeString(75);

            UserDTO userDTO = new UserDTO()
            {
                FirstName = largeString,
                LastName = "Dough"
            };

            bool result = helper.CheckModelValidation(userDTO);

            Assert.AreEqual(false, result);

            largeString = helper.GenerateLargeString(75);

            userDTO = new UserDTO()
            {
                FirstName = "Bob",
                LastName = largeString
            };

            result = helper.CheckModelValidation(userDTO);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void UserDTO_ShouldPassModelValidation_WhenRequiredFieldAreEntered()
        {
            UserDTO userDTO = new UserDTO()
            {
                FirstName = "Bob",
                LastName = "Dylan"
            };

            bool result = helper.CheckModelValidation(userDTO);

            Assert.AreEqual(true, result);
        }

    }
}
