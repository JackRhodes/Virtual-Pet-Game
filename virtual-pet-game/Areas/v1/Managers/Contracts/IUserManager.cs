using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Models.DTO;

namespace virtual_pet_game.Areas.v1.Managers.Contracts
{
    public interface IUserManager
    {
        IEnumerable<UserDTO> GetUsers();
        UserDTO GetUserById(int id);

        UserCreatedDTO AddUser(UserCreationDTO userCreationDTO);
    }
}
