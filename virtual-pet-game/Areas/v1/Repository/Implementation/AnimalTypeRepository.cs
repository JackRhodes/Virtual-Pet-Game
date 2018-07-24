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
            return context.AnimalTypes.OrderBy(x=>x.Id);
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

        public AnimalType UpdateAnimalType(int id, AnimalType animalType)
        {
            //With EFCore, I'd use the inbuilt Update methods.
            //I am replacing the list as System.Collection.Generic defined types are readonly.
            //The add method effectivly is the same as making a new list and changing the pointer.
            
            //I do not want the default value
            AnimalType result = context.AnimalTypes.First(x => x.Id == id);
            //Remove the result
            context.AnimalTypes.Remove(result);
            //Add the updated result
            context.AnimalTypes.Add(animalType);
           
            //Order is not maintained due to the limitation of lists not having an update method
            
            return context.AnimalTypes.First(x => x.Id == id);
        }
    }
}
