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
        ModelStateValidator validator;

        [TestInitialize]
        public void TestSetup()
        {
            validator = new ModelStateValidator();
        }
        [TestMethod]
        public void UserDTO_ShouldFailModelValidation_WhenRequiredFieldNotEntered()
        {
            UserDTO userDTO = new UserDTO()
            {
                FirstName = "Bob"
            };

            bool result = validator.CheckModelValidation(userDTO);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void UserDTO_ShouldFailModelValidation_WhenFieldsOverMaxLength()
        {
            StringBuilder invalidFirstName = new StringBuilder();

            string characters = "abcdefghijklmnopqrstuvwxyz";

            Random random = new Random();

            for (int i = 0; i < 75; i++)
            {
                invalidFirstName.Append(characters[random.Next(characters.Length - 1)]);
            }

            UserDTO userDTO = new UserDTO()
            {
                FirstName = invalidFirstName.ToString(),
                LastName = "Dough"
            };

            bool result = validator.CheckModelValidation(userDTO);

            Assert.AreEqual(false, result);

            userDTO = new UserDTO()
            {
                FirstName = "Bob",
                LastName = invalidFirstName.ToString()
            };

            result = validator.CheckModelValidation(userDTO);

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

            bool result = validator.CheckModelValidation(userDTO);

            Assert.AreEqual(true, result);
        }

    }
}
