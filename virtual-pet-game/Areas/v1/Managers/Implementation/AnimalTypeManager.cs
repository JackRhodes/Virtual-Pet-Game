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

        public AnimalTypeCreatedDTO CreateAnimalType(AnimalTypeCreationDTO animalTypeCreationDTO)
        {
            AnimalType animalType = Mapper.Map<AnimalType>(animalTypeCreationDTO);
            animalType.Id = GetNextId();

            AnimalType result = animalTypeRepository.CreateAnimalType(animalType);

            AnimalTypeCreatedDTO returnValue = Mapper.Map<AnimalTypeCreatedDTO>(result);

            return returnValue;
        }

        public AnimalTypeDTO GetAnimalTypeById(int id)
        {
            AnimalType animalType = animalTypeRepository.GetAnimalTypeById(id);
            AnimalTypeDTO returnValue = Mapper.Map<AnimalTypeDTO>(animalType);

            return returnValue;
        }

        public IEnumerable<AnimalTypeDTO> GetAnimalTypes()
        {
            IEnumerable<AnimalType> response = animalTypeRepository.GetAnimalTypes();
            IEnumerable <AnimalTypeDTO> returnValue =  Mapper.Map<IEnumerable<AnimalTypeDTO>>(response);

            return returnValue;
        }

        //Not needed when Database with sequential fields, added as using in memory database.
        private int GetNextId()
        {
            int maxId = animalTypeRepository.GetAnimalTypes().Max(x => x.Id);

            return maxId + 1;
        }

    }
}
