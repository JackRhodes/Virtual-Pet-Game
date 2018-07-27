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

        private const int DEFAULT_HUNGER = 50;
        private const int DEFAULT_HAPPINESS = 50;

        public AnimalManager(IAnimalRepository animalRepository)
        {
            this.animalRepository = animalRepository;
        }

        public AnimalDTO CreateAnimal(int userId, AnimalCreationDTO animalCreationDTO)
        {
            Animal animal = Mapper.Map<Animal>(animalCreationDTO);

            animal.Id = GetNextId();
            animal.UserId = userId;
            animal.LastChecked = DateTime.Now;
            animal.Happiness = DEFAULT_HAPPINESS;
            animal.Hunger = DEFAULT_HUNGER;

            //Create animal and convert to AnimalDTO
            AnimalDTO returnValue = Mapper.Map<AnimalDTO>(animalRepository.CreateAnimal(animal));

            return returnValue;
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

        //Not needed when Database with sequential fields, added as using in memory database.
        //This could be turned into a generic function
        private int GetNextId()
        {
            int maxId = animalRepository.GetNumberOfAnimals();

            return maxId + 1;
        }

        public void DeleteAnimal(int animalId)
        {
            Animal animal = animalRepository.GetAnimalById(animalId);
            animalRepository.DeleteAnimal(animal);
        }
    }
}
