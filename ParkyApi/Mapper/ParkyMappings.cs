using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ParkyApi.Models;
using ParkyApi.Models.DTO;

namespace ParkyApi.Mapper
{
    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            CreateMap<NationalPark, NationalPArkDTO>().ReverseMap();
           
        } 

    }
}
