using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using virtual_pet_game.Areas.v1.Managers.Contracts;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Models.DTO;
using virtual_pet_game_Areas.v1.Repository.Contracts;

namespace virtual_pet_game.Areas.v1.Managers.Implementation
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable<User> result = userRepository.GetUsers();
            IEnumerable<UserDTO> returnValue =  Mapper.Map<IEnumerable<UserDTO>>(result);

            return returnValue;            
        }
    }
}
