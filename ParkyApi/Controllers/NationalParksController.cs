﻿using System;
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
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParksController : Controller
    {
        private readonly INationalRepository _nationalRepository;
        private readonly IMapper _mapper;


#pragma warning disable 1591
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
        public IActionResult GetNationalPark()
        {
            var obList = _nationalRepository.GetNationalParks();
            var objDtp = new List<NationalPArkDTO>();
             
            foreach (var obj in obList)
            {
                objDtp.Add(_mapper.Map<NationalPArkDTO>(obj));
                
            }
            return Ok(objDtp);

        }
        /// <summary>
        /// Get individual park
        /// </summary>
        /// <param name="nationalPark">tyhe id of national park</param>
        /// <returns></returns>

        [HttpGet("{nationalPark:int}", Name = "NationalPark")]
        public IActionResult GetNationalPark(int nationalPark)
        {
            var obj = _nationalRepository.GetNationalPark(nationalPark);
            if (obj is null) return NotFound();
            var ObjDto = _mapper.Map<NationalPArkDTO>(obj);
            return Ok(ObjDto);

        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalPArkDTO nationalPArkDto)
        {
            if (nationalPArkDto is null) return BadRequest(ModelState);
            if (_nationalRepository.NationalParkExist(nationalPArkDto.Name))
            {
                ModelState.AddModelError("","Already exist");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var nationalParkobj = _mapper.Map<NationalPark>(nationalPArkDto);
            if (!_nationalRepository.CreateNationalPark(nationalParkobj))
            {
                ModelState.AddModelError("", $"{nationalParkobj.Name} cannot be saved");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("NationalPark", new { nationalPark = nationalParkobj.Id}, nationalParkobj);


        }


        [HttpPatch("{nationalPark:int}", Name = "UpdateNationalPark")]
        public IActionResult UpdateNationalPark(int nationalPark, [FromBody] NationalPArkDTO nationalPArkDto)
        {
            if (nationalPArkDto is null || nationalPark != nationalPArkDto.Id) return BadRequest(ModelState);
            var nationalParkobj = _mapper.Map<NationalPark>(nationalPArkDto);
            if (!_nationalRepository.UpdateNationalPark(nationalParkobj))
            {
                ModelState.AddModelError("", $"{nationalParkobj.Name} cannot be saved");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        
       
        [HttpDelete("{nationalParkId:int}", Name = "DeeleteNationalPark")]
        public IActionResult DeeleteNationalPark(int nationalParkId)
        {
            if (!_nationalRepository.NationalParkExist(nationalParkId))
            {
                return NotFound();
            }

            var nationalparkObj = _nationalRepository.GetNationalPark(nationalParkId);
            if (!_nationalRepository.DeleteNationalPark(nationalparkObj))
            {
                ModelState.AddModelError("","something went wrong");
                return StatusCode(500, ModelState);
            }

            return NoContent();

         }


    }

#pragma warning restore 1591
}
