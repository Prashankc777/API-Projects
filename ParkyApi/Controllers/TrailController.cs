using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyApi.Models;
using ParkyApi.Models.DTO;
using ParkyApi.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    //[Route("api/Trails")]
    [Route("api/v{version:apiVersion}/trails")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecTrails")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TrailController : ControllerBase
    {
        private readonly IParkyRepository _trailRepo;
        private readonly IMapper _mapper;

        public TrailController(IParkyRepository trailRepo, IMapper mapper)
        {
            _trailRepo = trailRepo;
            _mapper = mapper;
        }


        /// <summary>
        /// Get list of trails.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TrailDTo>))]
        public IActionResult GetTrails()
        {
            var objList = _trailRepo.GetTrail();
            var objDto = new List<TrailDTo>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<TrailDTo>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual trail
        /// </summary>
        /// <param name="trailId"> The id of the trail </param>
        /// <returns></returns>
        [HttpGet("{trailId:int}", Name = "GetTrail")]
        [ProducesResponseType(200, Type = typeof(TrailDTo))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Admin")]
        public IActionResult GetTrail(int trailId)
        {
            var obj = _trailRepo.GetTrail(trailId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<TrailDTo>(obj);

            return Ok(objDto);

        }

        [HttpGet("[action]/{nationalParkId:int}")]
        [ProducesResponseType(200, Type = typeof(TrailDTo))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrailInNationalPark(int nationalParkId)
        {
            var objList = _trailRepo.GetTrailsInNationalPark(nationalParkId);
            if (objList == null)
            {
                return NotFound();
            }
            var objDto = new List<TrailDTo>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<TrailDTo>(obj));
            }


            return Ok(objDto);

        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TrailDTo))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrail([FromBody] trailCreateDTO trailDto)
        {
            if (trailDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_trailRepo.trialExist(trailDto.Name))
            {
                ModelState.AddModelError("", "Trail Exists!");
                return StatusCode(404, ModelState);
            }
            var trailObj = _mapper.Map<Trail>(trailDto);
            if (!_trailRepo.CreateTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTrail", new { trailId = trailObj.Id }, trailObj);
        }

        [HttpPatch("{trailId:int}", Name = "UpdateTrail")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int trailId, [FromBody] trailUpdateDTO trailDto)
        {
            if (trailDto == null || trailId != trailDto.Id)
            {
                return BadRequest(ModelState);
            }

            var trailObj = _mapper.Map<Trail>(trailDto);
            if (!_trailRepo.Updatetrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        [HttpDelete("{trailId:int}", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int trailId)
        {
            if (!_trailRepo.trialExist(trailId))
            {
                return NotFound();
            }

            var trailObj = _trailRepo.GetTrail(trailId);
            if (!_trailRepo.Deletetrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}