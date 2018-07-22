using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace virtual_pet_game.Areas.v1.Models.DTO
{
    public class AnimalTypeCreationDTO
    {
        [Required]
        [MaxLength(30)]
        public string AnimalTypeName { get; set; }
        [Required]
        [Range(1,10, ErrorMessage = "Enter a value between 1 and 10")]
        public int HungerIncreaseRate { get; set; }
        [Range(1, 10, ErrorMessage = "Enter a value between 1 and 10")]
        public int HappinessDeductionRate { get; set; }
    }
}
