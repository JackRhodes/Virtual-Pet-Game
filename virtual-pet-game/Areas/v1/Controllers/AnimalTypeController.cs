using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using virtual_pet_game.Areas.v1.Managers.Contracts;
using virtual_pet_game.Areas.v1.Models.DTO;

namespace virtual_pet_game.Areas.v1.Controllers
{
    [Route("api/v1/animaltypes")]
    [ApiController]
    public class AnimalTypeController : ControllerBase
    {
        private readonly IAnimalTypeManager animalTypeManager;

        public AnimalTypeController(IAnimalTypeManager animalTypeManager )
        {
            this.animalTypeManager = animalTypeManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<AnimalTypeDTO> animalTypes = animalTypeManager.GetAnimalTypes();

            return Ok(animalTypes);
        }

        [HttpGet("{id}", Name = "GetAnimalTypeById")]
        public IActionResult GetById(int id)
        {
            AnimalTypeDTO animalType = null;

            try
            {
                animalType = animalTypeManager.GetAnimalTypeById(id);
            }
            //Manager will throw exceptions to manage responses
            catch (InvalidOperationException)
            {
                return NotFound($"{id} was not found");
            }

            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok(animalType);

        }

    }
}