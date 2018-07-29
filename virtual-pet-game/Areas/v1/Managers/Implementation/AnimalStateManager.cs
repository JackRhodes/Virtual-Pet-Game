using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Managers.Contracts;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Models.DTO;

namespace virtual_pet_game.Areas.v1.Managers.Implementation
{
    public class AnimalStateManager : IAnimalStateManager
    {

        public Animal CalculateAnimalState(Animal animal, AnimalTypeDTO animalType)
        {
            //Find the length of time since the last request
            TimeSpan timeSpan = DateTime.Now - animal.LastChecked;

            //Find total minutes between last request.
            //This calculate method is simple so will use Ints
            int minutes = Convert.ToInt32(timeSpan.TotalMinutes);

            //Ensure time has passed.
            if (minutes > 0)
            {
                try
                {
                    //Check for overflow... Someone could play this game once and then play it in 100 years
                    animal.Hunger = checked(animal.Hunger + (minutes * animalType.HungerIncreaseRate));
                }
                catch (OverflowException)
                {
                    animal.Hunger = 100;
                }

                try
                {
                    animal.Happiness = checked(animal.Happiness - (minutes * animalType.HappinessDeductionRate));
                }
                catch (OverflowException)
                {
                    animal.Happiness = 0;
                }

                animal.LastChecked = DateTime.Now;
            }
            //Check if values have gone below correct range

            if (animal.Hunger > 100)
                animal.Hunger = 100;
            else if (animal.Hunger < 0)
                animal.Hunger = 0;

            if (animal.Happiness > 100)
                animal.Happiness = 100;
            else if (animal.Happiness < 0)
                animal.Happiness = 0;

            return animal;
        }

        public AnimalDataTypes.AnimalState GetAnimalState(Animal animal)
        {
            int value = (100 - animal.Hunger) + animal.Happiness;

            AnimalDataTypes.AnimalState animalState;

            //This is a very simplistic implentation. 
            //This could be refactored to use a Database

            if (value >= 160)
                animalState = AnimalDataTypes.AnimalState.Euphoric;
            else if (value >= 120)
                animalState = AnimalDataTypes.AnimalState.Happy;
            else if (value >= 80)
                animalState = AnimalDataTypes.AnimalState.Neutral;
            else if (value >= 40)
                animalState = AnimalDataTypes.AnimalState.Stroppy;
            else
                animalState = AnimalDataTypes.AnimalState.Unhappy;

            //I do not like to do multiple return statements in a method.
            //It is much easier when debugging to put a breakpoint on the line below than cycling through each of the above.
            return animalState;
        }

        public Animal PetAnimal(Animal animal, AnimalTypeDTO animalType)
        {
            //This could be expanded drastically to incorporate Animal Happiness objects that increase happiness more depending upon their type.

            animal.Happiness += 20;

            return CalculateAnimalState(animal, animalType);            
        }

        public Animal FeedAnimal(Animal animal, AnimalTypeDTO animalType)
        {
            //This could be expanded drastically to incorporate Animal Food objects that decrease food more depending upon their type.

            animal.Hunger -= 20;

            return CalculateAnimalState(animal, animalType);
        }
    }
}
