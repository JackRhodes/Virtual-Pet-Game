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
    [Route("api/v1/user")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IUserManager userManager;
        private readonly IAnimalManager animalManager;

        public AnimalController(IUserManager userManager, IAnimalManager animalManager)
        {
            this.userManager = userManager;
            this.animalManager = animalManager;
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
    }
}