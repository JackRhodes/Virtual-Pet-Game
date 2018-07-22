using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtual_pet_game.Areas.v1.Models.Data
{
    public class Animal
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //When I use EF, I tend to have issues with adding object references
        //Therefore I am adding Id references and will let code handle relationship
        public int UserId { get; set; }

        public int AnimalTypeId { get; set; }
        public int Happiness { get; set; }
        public int Sadness { get; set; }
    }
}
