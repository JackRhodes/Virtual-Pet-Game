using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Models.DTO;
using virtual_pet_game.Areas.v1.Repository.Contracts;

namespace virtual_pet_game.Areas.v1.Repository.Implementation
{
    public class AnimalTypeRepository : IAnimalTypeRepository
    {
        private readonly IContext context;

        public AnimalTypeRepository(IContext context)
        {
            this.context = context;
        }

        public AnimalType GetAnimalTypeById(int id)
        {
            return context.AnimalTypes.First(x => x.Id == id);
        }

        public IEnumerable<AnimalType> GetAnimalTypes()
        {
            return context.AnimalTypes;
        }

        public AnimalType CreateAnimalType(AnimalType animalType)
        {
            context.AnimalTypes.Add(animalType);
            return GetAnimalTypeById(animalType.Id);
        }

        public void DeleteAnimalType(AnimalType animalType)
        {
            context.AnimalTypes.Remove(animalType);
        }
    }
}
