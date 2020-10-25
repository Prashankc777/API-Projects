using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkyWeb.Models;
using ParkyWeb.Repository.Interface;

using ParkyWeb.Repository.IRepositorys;

namespace ParkyWeb.Controllers
{
    public class NationalParkController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;


        public NationalParkController(INationalParkRepository nationalParkRepository)
        {
           _nationalParkRepository = nationalParkRepository;
        }

        public IActionResult Index()
        {
            return View(new NationalPArk() { });
        }

        public async Task<IActionResult> GetAllNationalPark()
        {
            return Json(new {data = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath)});
        }
    }
}
