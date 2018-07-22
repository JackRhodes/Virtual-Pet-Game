using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using virtual_pet_game.Areas.v1.Models.Data;

namespace virtual_pet_game_Areas.v1.Repository.Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);

        User CreateUser(User user);
    }
}
