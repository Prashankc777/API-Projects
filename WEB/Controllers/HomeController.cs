using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkyWeb;
using WEB.IRepositorys;
using WEB.Models;
using WEB.Models.ViewModel;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        public ITrailRepository TrailRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly INationalRepository _nationalRepository;

        public HomeController(ILogger<HomeController> logger, INationalRepository nationalRepository, ITrailRepository trailRepository)
        {
            TrailRepository = trailRepository;
            _logger = logger;
            _nationalRepository = nationalRepository;
        }

        public async Task<IActionResult> Index()
        {
            var listOfAll = new Index_VM()
            {
                NationalParkList = await _nationalRepository.GetAllAsync(SD.NationalParkAPIPath),
                Trails = await TrailRepository.GetAllAsync(SD.TrailAPIPath)
            };
            return View(listOfAll);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
