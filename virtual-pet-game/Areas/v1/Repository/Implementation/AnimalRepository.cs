﻿using System.Collections.Generic;
using System.Linq;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Repository.Contracts;

namespace virtual_pet_game.Areas.v1.Repository.Implementation
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly IContext context;

        public AnimalRepository(IContext context)
        {
            this.context = context;
        }

        public Animal CreateAnimal(Animal animal)
        {
            context.Animals.Add(animal);
            return GetAnimalById(animal.Id);
        }

        public Animal GetAnimalById(int id)
        {
            return context.Animals.First(x => x.Id == id);
        }

        public IEnumerable<Animal> GetAnimalsFromUser(int id)
        {
            return context.Animals.Where(x => x.UserId == id);
        }

        public int GetNumberOfAnimals()
        {
           return context.Animals.Max(x => x.Id);
        }
    }
}