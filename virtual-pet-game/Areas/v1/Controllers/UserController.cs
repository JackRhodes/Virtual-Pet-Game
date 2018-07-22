using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using virtual_pet_game.Areas.v1.Managers.Contracts;
using virtual_pet_game.Areas.v1.Models.DTO;

namespace virtual_pet_game.Areas.v1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        // GET: api/User
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<UserDTO> users = userManager.GetUsers();

            return Ok(users);
        }
        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            UserDTO user = null;

            try
            {
                user = userManager.GetUserById(id);
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

            return Ok(user);

        }
        [HttpPost]
        public IActionResult AddUser([FromBody] UserCreationDTO user)
        {
            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var returnValue = userManager.AddUser(user);
            
            return CreatedAtRoute("GetById", new {id = returnValue.Id }, returnValue);
        }


        //For the sake of time, I am omitting Patch and Delete.

    }
}
