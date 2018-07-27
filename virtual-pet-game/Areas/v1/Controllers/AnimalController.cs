using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using virtual_pet_game.Areas.v1.Exceptions;
using virtual_pet_game.Areas.v1.Managers.Contracts;
using virtual_pet_game.Areas.v1.Models.DTO;

namespace virtual_pet_game.Areas.v1.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IUserManager userManager;
        private readonly IAnimalManager animalManager;
        private readonly IAnimalTypeManager animalTypeManager;

        public AnimalController(IUserManager userManager, IAnimalManager animalManager, IAnimalTypeManager animalTypeManager)
        {
            this.userManager = userManager;
            this.animalManager = animalManager;
            this.animalTypeManager = animalTypeManager;
        }

        [HttpGet("{userId}/animals")]
        public IActionResult Get(int userId)
        {
            try
            {
                userManager.GetUserById(userId);
            }
            //Manager will throw exceptions to manage responses
            catch (InvalidOperationException)
            {
                return NotFound($"User: {userId} was not found");
            }

            catch (Exception)
            {
                return StatusCode(500);
            }

            IEnumerable<AnimalDTO> animalDTOs = animalManager.GetAnimalsByUserId(userId);

            return Ok(animalDTOs);
        }

        [HttpGet("{userId}/animals/{animalId}", Name = "GetAnimalById")]
        public IActionResult GetById(int userId, int animalId)
        {

            AnimalDTO animal = null;

            try
            {
                userManager.GetUserById(userId);
            }
            //Manager will throw exceptions to manage responses
            catch (InvalidOperationException)
            {
                return NotFound($"User: {userId} was not found");
            }

            catch (Exception)
            {
                return StatusCode(500);
            }

            try
            {
                animal = animalManager.GetAnimalById(userId,animalId);
            }
            //Manager will throw exceptions to manage responses
            catch (InvalidOperationException)
            {
                return NotFound($"Animal: {animalId} was not found");
            }
            catch(ResourceNotOwnedException)
            {
                return NotFound($"Animal: {animalId} was not found");
            }

            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok(animal);
        }

        [HttpPost("{userId}/animals")]
        public IActionResult Create(int userId, [FromBody]AnimalCreationDTO animalCreation)
        {
            //Check object states
            if (animalCreation == null)
                return BadRequest();
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                animalTypeManager.GetAnimalTypeById(animalCreation.AnimalTypeId);
            }
            catch(InvalidOperationException)
            {
                return NotFound($"AnimalType: {animalCreation.AnimalTypeId} was not found");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            //Check if User exists
            try
            {
                userManager.GetUserById(userId);
            }
            //Manager will throw exceptions to manage responses
            catch (InvalidOperationException)
            {
                return NotFound($"User: {userId} was not found");
            }

            catch (Exception)
            {
                return StatusCode(500);
            }


            var returnValue = animalManager.CreateAnimal(userId, animalCreation);

            return CreatedAtRoute("GetAnimalById", new {userId, animalId = returnValue.Id }, returnValue);

        }

        [HttpDelete("{userId}/animals/{animalId}")]
        public IActionResult Delete(int userId, int animalId)
        {
            //Check if User exists
            try
            {
                userManager.GetUserById(userId);
            }
            //Manager will throw exceptions to manage responses
            catch (InvalidOperationException)
            {
                return NotFound($"User: {userId} was not found");
            }

            catch (Exception)
            {
                return StatusCode(500);
            }
            
            try
            {
                animalManager.GetAnimalById(userId,animalId);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"Animal: {animalId} was not found");
            }
            catch (ResourceNotOwnedException)
            {
                return NotFound($"Animal: {animalId} was not found");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            animalManager.DeleteAnimal(animalId);

            return NoContent();
        }

    }
}