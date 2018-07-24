using Microsoft.VisualStudio.TestTools.UnitTesting;
using virtual_pet_game.Areas.v1.Models.DTO;
using virtual_pet_game.Tests.v1.Models.Helper;

namespace virtual_pet_game.Tests.v1.Models
{
    [TestClass]
    public class UserDTOTests
    {
        [TestInitialize]
        public void TestSetup()
        {

        }
        [TestMethod]
        public void UserDTO_ShouldFailModelValidation_WhenRequiredFieldNotEntered()
        {
            UserDTO userDTO = new UserDTO()
            {
                FirstName = "Bob"
            };

            bool result = HelperMethods.CheckModelValidation(userDTO);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void UserDTO_ShouldFailModelValidation_WhenFieldsOverMaxLength()
        {

            string largeString = HelperMethods.GenerateLargeString(75);

            UserDTO userDTO = new UserDTO()
            {
                FirstName = largeString,
                LastName = "Dough"
            };

            bool result = HelperMethods.CheckModelValidation(userDTO);

            Assert.AreEqual(false, result);

            largeString = HelperMethods.GenerateLargeString(75);

            userDTO = new UserDTO()
            {
                FirstName = "Bob",
                LastName = largeString
            };

            result = HelperMethods.CheckModelValidation(userDTO);

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

            bool result = HelperMethods.CheckModelValidation(userDTO);

            Assert.AreEqual(true, result);
        }

    }
}
