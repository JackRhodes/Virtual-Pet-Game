using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using virtual_pet_game.Areas.v1.Models.Data;

namespace virtual_pet_game.Areas.v1.Data
{
    public class StubDataContext:IContext
    {
        public List<User> Users { get; set; } = new List<User>()
        {
            new User()
            {
             Id = 1,
             FirstName = "Jack",
             LastName = "Rhodes",
             Password = "fooBar"
            },

            new User()
            {

             Id = 2,
             FirstName = "Elvis",
             LastName = "Presley",
             Password = "foofooBar"
            },

        };
    }
}
