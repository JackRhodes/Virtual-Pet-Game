using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using virtual_pet_game.Areas.v1.Models.DTO;
using virtual_pet_game.Tests.v1.Models.Helper;

namespace virtual_pet_game.Tests.v1.Models
{
    [TestClass]
    public class AnimalTypeCreationDTOTests
    {
        HelperMethods helper;

        [TestInitialize]
        public void TestSetup()
        {
            helper = new HelperMethods();
        }
        [TestMethod]
        public void AnimalTypeCreationDTO_ShouldFailModelValidation_WhenRequiredFieldNotEntered()
        {
            AnimalTypeCreationDTO animalTypeCreationDTO = new AnimalTypeCreationDTO()
            {
                AnimalTypeName = "Rabbit"
            };

            bool result = helper.CheckModelValidation(animalTypeCreationDTO);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AnimalTypeCreationDTO_ShouldFailModelValidation_WhenFieldsOverMaxLength()
        {
            string largeString = helper.GenerateLargeString(100);

            AnimalTypeCreationDTO animalTypeCreation = new AnimalTypeCreationDTO()
            {
                AnimalTypeName = largeString,
                HappinessDeductionRate = 1,
                HungerIncreaseRate = 1
            };

            bool result = helper.CheckModelValidation(animalTypeCreation);

            Assert.IsFalse(result);

            animalTypeCreation = new AnimalTypeCreationDTO()
            {
                AnimalTypeName = "Bunny",
                HappinessDeductionRate = 100,
                HungerIncreaseRate = 1
            };

            result = helper.CheckModelValidation(animalTypeCreation);

            Assert.IsFalse(result);

            animalTypeCreation = new AnimalTypeCreationDTO()
            {
                AnimalTypeName = "Bunny",
                HappinessDeductionRate = 1,
                HungerIncreaseRate = 100
            };

            result = helper.CheckModelValidation(animalTypeCreation);

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

            bool result = helper.CheckModelValidation(AnimalTypeCreation);

            Assert.IsTrue(result);
        }
    }
}
