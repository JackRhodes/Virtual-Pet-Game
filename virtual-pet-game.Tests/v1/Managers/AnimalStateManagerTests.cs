using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Managers.Contracts;
using virtual_pet_game.Areas.v1.Managers.Implementation;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Models.DTO;

namespace virtual_pet_game.Tests.v1.Managers
{
    [TestClass]
    public class AnimalStateManagerTests
    {
        private readonly IAnimalStateManager animalStateManager = new AnimalStateManager();

        //These tests use DateTime. It is possible that they will periodically fail if ran near the change of a minute
        
        [TestMethod]
        public void CalculateAnimalState_ShouldChangeHappinessAndHunger_WhenCalled()
        {
            Animal animal = new Animal()
            {
                AnimalTypeId = 1,
                Happiness = 50,
                Hunger = 50,
                Id = 1,
                LastChecked = DateTime.Now.AddMinutes(-5),
                Name = "Albie",
                UserId = 1
            };

            AnimalTypeDTO animalType = new AnimalTypeDTO()
            {
                AnimalTypeName = "Dog",
                HappinessDeductionRate = 3,
                HungerIncreaseRate = 2,
                Id = 1
            };
            
            Animal returnAnimal = animalStateManager.CalculateAnimalState(animal, animalType);

            Assert.AreEqual(60, returnAnimal.Hunger);
            Assert.AreEqual(35, returnAnimal.Happiness);
        }

        [TestMethod]
        public void CalculateAnimalSate_ShouldReturnMaxValues_WhenCalledOutsideMaxRange()
        {
            Animal animal = new Animal()
            {
                AnimalTypeId = 1,
                Happiness = 50,
                Hunger = 50,
                Id = 1,
                LastChecked = DateTime.Now.AddMinutes(-500000),
                Name = "Milo",
                UserId = 1
            };

            AnimalTypeDTO animalType = new AnimalTypeDTO()
            {
                AnimalTypeName = "Dog",
                HappinessDeductionRate = 3,
                HungerIncreaseRate = 2,
                Id = 1
            };

            Animal returnAnimal = animalStateManager.CalculateAnimalState(animal, animalType);

            Assert.AreEqual(100, returnAnimal.Hunger);
            Assert.AreEqual(0, returnAnimal.Happiness);
        }


        [TestMethod]
        public void CalculateAnimalSate_ShouldReturnMaxValues_WhenCalledWithLowestPossibleDateTime()
        {
            Animal animal = new Animal()
            {
                AnimalTypeId = 1,
                Happiness = 50,
                Hunger = 50,
                Id = 1,
                LastChecked = DateTime.MinValue,
                Name = "Molly",
                UserId = 1
            };

            AnimalTypeDTO animalType = new AnimalTypeDTO()
            {
                AnimalTypeName = "Dog",
                HappinessDeductionRate = 3,
                HungerIncreaseRate = 2,
                Id = 1
            };

            Animal returnAnimal = animalStateManager.CalculateAnimalState(animal, animalType);

            Assert.AreEqual(100, returnAnimal.Hunger);
            Assert.AreEqual(0, returnAnimal.Happiness);
        }

        [TestMethod]
        public void GetAnimalState_ShouldReturnEuphoric_WhenValidEuphoricValues()
        {
            Animal animal = new Animal()
            {
                AnimalTypeId = 1,
                Happiness = 87,
                Hunger = 9,
                Id = 1,
                LastChecked = DateTime.MinValue,
                Name = "Molly",
                UserId = 1
            };

            AnimalDataTypes.AnimalState animalState = animalStateManager.GetAnimalState(animal);

            Assert.IsNotNull(animalState);
            Assert.AreEqual(AnimalDataTypes.AnimalState.Euphoric, animalState);

        }
        
        [TestMethod]
        public void GetAnimalState_ShouldReturnHappy_WhenValidHappyValues()
        {
            Animal animal = new Animal()
            {
                AnimalTypeId = 1,
                Happiness = 80,
                Hunger = 40,
                Id = 1,
                LastChecked = DateTime.MinValue,
                Name = "Molly",
                UserId = 1
            };

            AnimalDataTypes.AnimalState animalState = animalStateManager.GetAnimalState(animal);

            Assert.IsNotNull(animalState);
            Assert.AreEqual(AnimalDataTypes.AnimalState.Happy, animalState);
        }

        [TestMethod]
        public void GetAnimalState_ShouldReturnNeutral_WhenValidNeutralValues()
        {
            Animal animal = new Animal()
            {
                AnimalTypeId = 1,
                Happiness = 50,
                Hunger = 50,
                Id = 1,
                LastChecked = DateTime.MinValue,
                Name = "Molly",
                UserId = 1
            };

            AnimalDataTypes.AnimalState animalState = animalStateManager.GetAnimalState(animal);

            Assert.IsNotNull(animalState);
            Assert.AreEqual(AnimalDataTypes.AnimalState.Neutral, animalState);
        }

        [TestMethod]
        public void GetAnimalState_ShouldReturnStroppy_WhenValidStroppyValues()
        {
            Animal animal = new Animal()
            {
                AnimalTypeId = 1,
                Happiness = 30,
                Hunger = 80,
                Id = 1,
                LastChecked = DateTime.MinValue,
                Name = "Molly",
                UserId = 1
            };

            AnimalDataTypes.AnimalState animalState = animalStateManager.GetAnimalState(animal);

            Assert.IsNotNull(animalState);
            Assert.AreEqual(AnimalDataTypes.AnimalState.Stroppy, animalState);
        }

        [TestMethod]
        public void GetAnimalState_ShouldReturnUnahppy_WhenValidUnhappyValues()
        {
            Animal animal = new Animal()
            {
                AnimalTypeId = 1,
                Happiness = 0,
                Hunger = 80,
                Id = 1,
                LastChecked = DateTime.MinValue,
                Name = "Molly",
                UserId = 1
            };

            AnimalDataTypes.AnimalState animalState = animalStateManager.GetAnimalState(animal);

            Assert.IsNotNull(animalState);
            Assert.AreEqual(AnimalDataTypes.AnimalState.Unhappy, animalState);
        }

        [TestMethod]
        public void PetAnimal_ShouldIncreaseHappiness_WhenPetted()
        {
            Animal animal = new Animal()
            {
                AnimalTypeId = 1,
                Happiness = 0,
                Hunger = 40,
                Id = 1,
                LastChecked = DateTime.Now,
                Name = "Molly",
                UserId = 1
            };


            AnimalTypeDTO animalType = new AnimalTypeDTO()
            {
                AnimalTypeName = "Dog",
                HappinessDeductionRate = 3,
                HungerIncreaseRate = 2,
                Id = 1
            };

            animal = animalStateManager.PetAnimal(animal, animalType);

            Assert.AreEqual(20, animal.Happiness);
        }

        [TestMethod]
        public void PetAnimal_ShouldBeMaxHappiness_WhenPettedAt100Happiness()
        {
            Animal animal = new Animal()
            {
                AnimalTypeId = 1,
                Happiness = 100,
                Hunger = 40,
                Id = 1,
                LastChecked = DateTime.Now,
                Name = "Molly",
                UserId = 1
            };


            AnimalTypeDTO animalType = new AnimalTypeDTO()
            {
                AnimalTypeName = "Dog",
                HappinessDeductionRate = 3,
                HungerIncreaseRate = 2,
                Id = 1
            };

            animal = animalStateManager.PetAnimal(animal, animalType);

            Assert.AreEqual(100, animal.Happiness);
        }

        [TestMethod]
        public void FeedAnimal_ShouldDecreaseHunger_WhenFed()
        {
            Animal animal = new Animal()
            {
                AnimalTypeId = 1,
                Happiness = 100,
                Hunger = 40,
                Id = 1,
                LastChecked = DateTime.Now,
                Name = "Molly",
                UserId = 1
            };


            AnimalTypeDTO animalType = new AnimalTypeDTO()
            {
                AnimalTypeName = "Dog",
                HappinessDeductionRate = 3,
                HungerIncreaseRate = 2,
                Id = 1
            };

            animal = animalStateManager.FeedAnimal(animal, animalType);

            Assert.AreEqual(20, animal.Hunger);
        }

        [TestMethod]
        public void FeedAnimal_ShouldBeMinimumHunger_WhenFedWhenFull()
        {
            Animal animal = new Animal()
            {
                AnimalTypeId = 1,
                Happiness = 100,
                Hunger = 0,
                Id = 1,
                LastChecked = DateTime.Now,
                Name = "Molly",
                UserId = 1
            };


            AnimalTypeDTO animalType = new AnimalTypeDTO()
            {
                AnimalTypeName = "Dog",
                HappinessDeductionRate = 3,
                HungerIncreaseRate = 2,
                Id = 1
            };

            animal = animalStateManager.FeedAnimal(animal, animalType);

            Assert.AreEqual(0, animal.Hunger);
        }


    }
}
