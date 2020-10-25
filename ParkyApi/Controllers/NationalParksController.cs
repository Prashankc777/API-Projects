using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyApi.Models;
using ParkyApi.Models.DTO;
using ParkyApi.Repository;

namespace ParkyApi.Controllers
{
    [Route("api/v{version:apiVersion}/nationalparks")]
    [ApiVersion("2.0")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "parkyOpenAPISpecNP")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NationalParksController : Controller
    {
        private readonly INationalRepository _nationalRepository;
        private readonly IMapper _mapper;



        public NationalParksController(INationalRepository nationalRepository, IMapper mapper)

        {
            this._nationalRepository = nationalRepository;
            this._mapper = mapper;   
        }

        /// <summary>
        /// Get List of all national Park
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NationalPark>))]
       
        public IActionResult GetNationalPark()
        {
            var obList = _nationalRepository.GetNationalParks().FirstOrDefault();
            return Ok(obList);

        }
       

    }

#pragma warning restore 1591
}
