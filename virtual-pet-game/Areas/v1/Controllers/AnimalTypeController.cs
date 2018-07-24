using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        public AnimalTypeController(IAnimalTypeManager animalTypeManager)
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

        [HttpPost]
        public IActionResult Create([FromBody] AnimalTypeCreationDTO user)
        {
            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var returnValue = animalTypeManager.CreateAnimalType(user);

            return CreatedAtRoute("GetAnimalTypeById", new { id = returnValue.Id }, returnValue);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                animalTypeManager.DeleteAnimalType(id);
            }

            catch (InvalidOperationException)
            {
                return BadRequest();
            }

            catch (Exception)
            {
                return StatusCode(500);
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AnimalTypeCreationDTO animalType)
        {
            if (animalType == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var returnValue = animalTypeManager.UpdateAnimalType(id, animalType);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"{id} was not found");
            }

            catch (Exception)
            {
                return StatusCode(500);
            }

            //As per API standards, returning NoContent();
            //Personally I think it should return a 201
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartialUpdate(int id, [FromBody] JsonPatchDocument<AnimalTypeCreationDTO> patch)
        {

            AnimalTypeCreationDTO animalTypeCreationDTO = null;

            if (patch == null)
                return BadRequest();
            try
            {
                animalTypeCreationDTO = animalTypeManager.GetFullAnimalTypeById(id);
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

            //Was initially going to add this as a manager method, however model state is built into controller making it much easier to handle here.
            patch.ApplyTo(animalTypeCreationDTO, ModelState);

            TryValidateModel(animalTypeCreationDTO);
            //If the updated model state is invalid, return bad request.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            animalTypeManager.UpdateAnimalType(id, animalTypeCreationDTO);

            return NoContent();
        }

    }
}