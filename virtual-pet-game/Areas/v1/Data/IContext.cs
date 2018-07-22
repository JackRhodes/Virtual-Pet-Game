using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using virtual_pet_game.Areas.v1.Models.Data;

namespace virtual_pet_game.Areas.v1.Data
{
    /// <summary>
    /// Replicating DBContext
    /// </summary>
    public interface IContext
    {
        //This is a list so I have access to the "Add" method.
        //In reality I'd be using EF Core so would have this as DBSet and then utilise EF Core LINQ extension methods.
        List<User> Users { get; set; }

        List<AnimalType> AnimalTypes { get; set; }
    }
}
