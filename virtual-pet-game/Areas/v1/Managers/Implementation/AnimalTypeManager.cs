using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using virtual_pet_game.Areas.v1.Managers.Contracts;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Models.DTO;
using virtual_pet_game.Areas.v1.Repository.Contracts;

namespace virtual_pet_game.Areas.v1.Managers.Implementation
{
    public class AnimalTypeManager : IAnimalTypeManager
    {
        private readonly IAnimalTypeRepository animalTypeRepository;

        public AnimalTypeManager(IAnimalTypeRepository animalTypeRepository)
        {
            this.animalTypeRepository = animalTypeRepository;
        }
        public IEnumerable<AnimalTypeDTO> GetAnimalTypes()
        {
            IEnumerable<AnimalType> response = animalTypeRepository.GetAnimals();
            IEnumerable <AnimalTypeDTO> returnValue =  Mapper.Map<IEnumerable<AnimalTypeDTO>>(response);

            return returnValue;
        }
    }
}
