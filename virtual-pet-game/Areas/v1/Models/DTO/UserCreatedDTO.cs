using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtual_pet_game.Areas.v1.Models.DTO
{
    public class UserCreatedDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
