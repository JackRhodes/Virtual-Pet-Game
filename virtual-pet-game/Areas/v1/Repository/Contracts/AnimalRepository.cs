using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Models.Data;

namespace virtual_pet_game.Areas.v1.Repository.Contracts
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly IContext context;

        public AnimalRepository(IContext context)
        {
            this.context = context;
        }

        public IEnumerable<Animal> GetAnimalsFromUser(int id)
        {
            return context.Animals.Where(x => x.UserId == id);
        }
    }
}
