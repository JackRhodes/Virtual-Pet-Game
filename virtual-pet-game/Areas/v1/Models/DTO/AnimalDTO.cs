using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace virtual_pet_game.Areas.v1.Models.DTO
{
    public class AnimalDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int AnimalTypeId { get; set; }
        [Required]
        public int Happiness { get; set; }
        [Required]
        public int Hunger { get; set; }
    }
}
