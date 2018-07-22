using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using virtual_pet_game.Areas.v1.Models.Data;
using virtual_pet_game.Areas.v1.Models.DTO;

namespace virtual_pet_game.Areas.v1.Models.Mappings
{
    public class DTOMappings: Profile
    {
        public DTOMappings()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserCreationDTO, User>();
            CreateMap<User, UserCreatedDTO>();
            CreateMap<AnimalType, AnimalTypeDTO>();
        }
    }
}
