﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using virtual_pet_game.Areas.v1.Models.Data;

namespace virtual_pet_game.Areas.v1.Data
{
    /// <summary>
    /// Replicating DBContext
    /// </summary>
    public interface IContext
    {
        IEnumerable<User> Users { get; set; }
    }
}
