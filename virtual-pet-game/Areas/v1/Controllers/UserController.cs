using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using virtual_pet_game.Areas.v1.Models.DTO;

namespace virtual_pet_game.Areas.v1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/User
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<UserDTO> users = new List<UserDTO>()
            {
                new UserDTO()
                {
                    FirstName = "Jack",
                    LastName = "Rhodes"
                },

                new UserDTO()
                {
                    FirstName = "Elvis",
                    LastName = "Presley"
                }
                
            };

            return Ok(users);
        }
        
    }
}
