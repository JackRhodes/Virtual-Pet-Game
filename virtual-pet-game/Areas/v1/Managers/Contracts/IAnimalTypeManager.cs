using System.Collections.Generic;
using virtual_pet_game.Areas.v1.Models.DTO;

namespace virtual_pet_game.Areas.v1.Managers.Contracts
{
    public interface IAnimalTypeManager
    {
        IEnumerable<AnimalTypeDTO> GetAnimalTypes();
    }
}
