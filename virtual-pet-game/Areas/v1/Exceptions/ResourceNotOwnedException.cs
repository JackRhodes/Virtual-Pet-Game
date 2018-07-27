using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace virtual_pet_game.Areas.v1.Exceptions
{
    public class ResourceNotOwnedException: Exception
    {
        public ResourceNotOwnedException() : base("The user does not own the specified Resource")
        {

        }
    }
}
