using Microsoft.VisualStudio.TestTools.UnitTesting;
using virtual_pet_game.Areas.v1.Models.DTO;
using virtual_pet_game.Tests.v1.Models.Helper;

namespace virtual_pet_game.Tests.v1.Models
{
    [TestClass]
    public class AnimalTypeCreationDTOTests
    {
        [TestInitialize]
        public void TestSetup()
        {
        }
        [TestMethod]
        public void AnimalTypeCreationDTO_ShouldFailModelValidation_WhenRequiredFieldNotEntered()
        {
            AnimalTypeCreationDTO animalTypeCreationDTO = new AnimalTypeCreationDTO()
            {
                AnimalTypeName = "Rabbit"
            };

            bool result = HelperMethods.CheckModelValidation(animalTypeCreationDTO);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AnimalTypeCreationDTO_ShouldFailModelValidation_WhenFieldsOverMaxLength()
        {
            string largeString = HelperMethods.GenerateLargeString(100);

            AnimalTypeCreationDTO animalTypeCreation = new AnimalTypeCreationDTO()
            {
                AnimalTypeName = largeString,
                HappinessDeductionRate = 1,
                HungerIncreaseRate = 1
            };

            bool result = HelperMethods.CheckModelValidation(animalTypeCreation);

            Assert.IsFalse(result);

            animalTypeCreation = new AnimalTypeCreationDTO()
            {
                AnimalTypeName = "Bunny",
                HappinessDeductionRate = 100,
                HungerIncreaseRate = 1
            };

            result = HelperMethods.CheckModelValidation(animalTypeCreation);

            Assert.IsFalse(result);

            animalTypeCreation = new AnimalTypeCreationDTO()
            {
                AnimalTypeName = "Bunny",
                HappinessDeductionRate = 1,
                HungerIncreaseRate = 100
            };

            result = HelperMethods.CheckModelValidation(animalTypeCreation);

        }

        [TestMethod]
        public void AnimalTypeCreationDTO_ShouldPassModelValidation_WhenRequiredFieldAreEntered()
        {
            AnimalTypeCreationDTO AnimalTypeCreation = new AnimalTypeCreationDTO()
            {
                AnimalTypeName = "Bunny",
                HappinessDeductionRate = 1,
                HungerIncreaseRate = 1
            };

            bool result = HelperMethods.CheckModelValidation(AnimalTypeCreation);

            Assert.IsTrue(result);
        }
    }
}
