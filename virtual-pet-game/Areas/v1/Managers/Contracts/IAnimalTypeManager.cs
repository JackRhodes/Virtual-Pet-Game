using System.Collections.Generic;
using virtual_pet_game.Areas.v1.Models.DTO;

namespace virtual_pet_game.Areas.v1.Managers.Contracts
{
    public interface IAnimalTypeManager
    {
        IEnumerable<AnimalTypeDTO> GetAnimalTypes();
        AnimalTypeDTO GetAnimalTypeById(int id);

        AnimalTypeCreatedDTO CreateAnimalType(AnimalTypeCreationDTO animalTypeCreationDTO);

        void DeleteAnimalType(int id);

        AnimalTypeCreatedDTO UpdateAnimalType(int id, AnimalTypeCreationDTO updatedAnimalType);
    }
}
