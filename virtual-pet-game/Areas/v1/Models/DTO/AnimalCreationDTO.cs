using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace virtual_pet_game.Areas.v1.Models.DTO
{
    public class AnimalCreationDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int AnimalTypeId { get; set; }
    }
}
