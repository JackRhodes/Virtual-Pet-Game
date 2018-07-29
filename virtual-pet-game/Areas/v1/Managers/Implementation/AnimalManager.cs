using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using virtual_pet_game.Areas.v1.Exceptions;
using virtual_pet_game.Areas.v1.Managers.Contracts;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Models.DTO;
using virtual_pet_game.Areas.v1.Repository.Contracts;

namespace virtual_pet_game.Areas.v1.Managers.Implementation
{
    public class AnimalManager : IAnimalManager
    {
        private readonly IAnimalRepository animalRepository;
        private readonly IAnimalTypeManager animalTypeManager;
        private readonly IAnimalStateManager animalStateManager;
        private const int DEFAULT_HUNGER = 50;
        private const int DEFAULT_HAPPINESS = 50;

        public AnimalManager(IAnimalRepository animalRepository,
            IAnimalTypeManager animalTypeManager,
            IAnimalStateManager animalStateManager)
        {
            this.animalRepository = animalRepository;
            this.animalTypeManager = animalTypeManager;
            this.animalStateManager = animalStateManager;
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

        public AnimalDTO GetAnimalById(int userId, int id)
        {
            Animal animal = animalRepository.GetAnimalById(id);

            if (userId != animal.UserId)
            {
                throw new ResourceNotOwnedException();
            }

            //Find the animalType associated with the animal
            AnimalTypeDTO animalType = animalTypeManager.GetAnimalTypeById(animal.AnimalTypeId);
            //Calculate the new animal state values
            animal = animalStateManager.CalculateAnimalState(animal, animalType);
            //Update the animal in the database
            animal = animalRepository.UpdateAnimal(animal);

            AnimalDTO returnValue = Mapper.Map<AnimalDTO>(animal);

            return returnValue;
        }

        public IEnumerable<AnimalDTO> GetAnimalsByUserId(int id)
        {
            //Create new reference in heap
            List<Animal> animals = animalRepository.GetAnimalsFromUser(id).ToList();

            List<Animal> updatedAnimals = new List<Animal>();


            foreach (var animal in animals)
            {
                AnimalTypeDTO animalType = animalTypeManager.GetAnimalTypeById(animal.AnimalTypeId);
                //Calculate the new animal state values

                Animal tempAnimal;

                tempAnimal = animalStateManager.CalculateAnimalState(animal, animalType);
                //Update the animal in the database
                tempAnimal = animalRepository.UpdateAnimal(tempAnimal);

                updatedAnimals.Add(tempAnimal);
            }


            IEnumerable<AnimalDTO> returnValue = Mapper.Map<IEnumerable<AnimalDTO>>(updatedAnimals);

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

        public AnimalDTO FeedAnimal(int userId, int id)
        {
            Animal animal = animalRepository.GetAnimalById(id);

            if (userId != animal.UserId)
            {
                throw new ResourceNotOwnedException();
            }

            //Find the animalType associated with the animal
            AnimalTypeDTO animalType = animalTypeManager.GetAnimalTypeById(animal.AnimalTypeId);

            animal = animalStateManager.FeedAnimal(animal, animalType);

            animal = animalRepository.UpdateAnimal(animal);

            AnimalDTO returnValue = Mapper.Map<AnimalDTO>(animal);

            return returnValue;
        }

        public AnimalDTO PetAnimal(int userId, int id)
        {
            Animal animal = animalRepository.GetAnimalById(id);

            if (userId != animal.UserId)
            {
                throw new ResourceNotOwnedException();
            }

            //Find the animalType associated with the animal
            AnimalTypeDTO animalType = animalTypeManager.GetAnimalTypeById(animal.AnimalTypeId);

            animal = animalStateManager.PetAnimal(animal, animalType);

            animal = animalRepository.UpdateAnimal(animal);

            AnimalDTO returnValue = Mapper.Map<AnimalDTO>(animal);

            return returnValue;
        }

    }
}
