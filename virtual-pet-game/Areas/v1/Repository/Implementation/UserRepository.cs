using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using virtual_pet_game.Areas.v1.Data;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game_Areas.v1.Repository.Contracts;

namespace virtual_pet_game_Areas.v1.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly IContext context;

        public UserRepository(IContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users;
        }
    }
}
