using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtual_pet_game.Areas.v1.Models.Data
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // I would not store a password like this in production code. 
        // I have included to show I know difference between DataModels and DTOs.        
        public string Password { get; set; }
    }
}
