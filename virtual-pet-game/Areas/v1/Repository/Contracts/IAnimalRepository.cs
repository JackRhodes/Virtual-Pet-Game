using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using virtual_pet_game.Areas.v1.Models.Data;

namespace virtual_pet_game.Areas.v1.Repository.Contracts
{
    public interface IAnimalRepository
    {
        IEnumerable<Animal> GetAnimalsFromUser(int id);
        Animal GetAnimalById(int id);
        Animal CreateAnimal(Animal animal);
        /// <summary>
        /// Not required with sequential database
        /// </summary>
        /// <returns></returns>
        int GetNumberOfAnimals();
    }
}
