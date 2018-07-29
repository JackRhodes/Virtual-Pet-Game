using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Models.DTO;

namespace virtual_pet_game.Areas.v1.Managers.Contracts
{
    public interface IAnimalStateManager
    {
        Animal CalculateAnimalState(Animal animal, AnimalTypeDTO animalType);
        AnimalDataTypes.AnimalState GetAnimalState(Animal animal);
        Animal PetAnimal(Animal animal, AnimalTypeDTO animalType);
        Animal FeedAnimal(Animal animal, AnimalTypeDTO animalType);
    }
}
