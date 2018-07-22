using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public UserCreatedDTO AddUser(UserCreationDTO userCreationDTO)
        {
            User user = Mapper.Map<User>(userCreationDTO);
            user.Id = GetNextId();

            User result = userRepository.CreateUser(user);

            UserCreatedDTO returnValue = Mapper.Map<UserCreatedDTO>(result);
            
            return returnValue;
        }

        public UserDTO GetUserById(int id)
        {
            User user = userRepository.GetUserById(id);
            UserDTO returnValue = Mapper.Map<UserDTO>(user);

            return returnValue;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable<User> result = userRepository.GetUsers();
            IEnumerable<UserDTO> returnValue = Mapper.Map<IEnumerable<UserDTO>>(result);

            return returnValue;
        }

        //Not needed when Database with sequential fields, added as using in memory database.
        private int GetNextId()
        {
            int maxId = userRepository.GetUsers().Max(x => x.Id);

            return maxId +1;
        }

    }
}
