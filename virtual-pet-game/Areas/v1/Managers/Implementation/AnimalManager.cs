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
    public class AnimalManager : IAnimalManager
    {
        private readonly IAnimalRepository animalRepository;

        public AnimalManager(IAnimalRepository animalRepository)
        {
            this.animalRepository = animalRepository;
        }

        public AnimalDTO GetAnimalById(int id)
        {
            Animal animal = animalRepository.GetAnimalById(id);

            AnimalDTO returnValue = Mapper.Map<AnimalDTO>(animal);

            return returnValue;
        }

        public IEnumerable<AnimalDTO> GetAnimalsByUserId(int id)
        {
            IEnumerable<Animal> animals = animalRepository.GetAnimalsFromUser(id);

            IEnumerable<AnimalDTO> returnValue = Mapper.Map<IEnumerable<AnimalDTO>>(animals);

            return returnValue;
        }
    }
}
