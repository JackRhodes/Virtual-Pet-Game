using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtual_pet_game.Areas.v1.Models.DTO
{
    public class AnimalTypeCreatedDTO
    {
        public int Id { get; set; }
        public string AnimalTypeName { get; set; }
        public int HungerIncreaseRate { get; set; }
        public int HappinessDeductionRate { get; set; }
    }
}
