using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyApi.Models;
using ParkyApi.Repository.IRepository;

namespace ParkyApi.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUSer _userRepository;


        public UsersController(IUSer uSer)
        {
            _userRepository = uSer;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenicate([FromBody] User model)
        {
            var user = _userRepository.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User modelUser)
        {

            if (_userRepository.IsUnique(modelUser.Username))
            {
                return BadRequest(new { message = "username already Exist" });

            }
            var user = _userRepository.Register(modelUser.Username, modelUser.Password);
            if (user is null)
            {
                return BadRequest(new { message = "Error while registering" });
            }

            return Ok();
        }




    }
}
