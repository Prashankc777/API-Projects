using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkyWeb;
using WEB.IRepositorys;
using WEB.Models;
using WEB.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB.Controllers
{
    public class TrailController : Controller
    {
        private readonly INationalRepository _nationalRepository;
        private readonly ITrailRepository _trailRepository;

        public TrailController(INationalRepository nationalRepository , ITrailRepository trailRepository)
        {
            _nationalRepository = nationalRepository;
            _trailRepository = trailRepository;
        }
        public IActionResult Index()
        {
            return View(new Trail() { });
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<NationalPark> npList = await _nationalRepository.GetAllAsync(SD.NationalParkAPIPath);
            TrailVm objVm = new TrailVm()
            {
                NationaParkList = npList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()

                }),
                Trail = new Trail()
            };

            if (id is null)
            { 
                return View(objVm);
            }

            objVm.Trail = await _trailRepository.GetAsync(SD.TrailAPIPath, id.GetValueOrDefault());
            if (objVm.Trail is null)
            {
                return NotFound();
            }

            return View(objVm);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(TrailVm obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Trail.Id == 0)
                {
                    await _trailRepository.CreateAsync(SD.TrailAPIPath, obj.Trail);
                }
                else
                {
                    await _trailRepository.UpdateAsync(SD.TrailAPIPath + obj.Trail.Id, obj.Trail);
                }

                return RedirectToAction(nameof(Index));
            }

            IEnumerable<NationalPark> npList = await _nationalRepository.GetAllAsync(SD.NationalParkAPIPath);
            TrailVm objVm = new TrailVm()
            {
                NationaParkList = npList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()

                }),
                Trail = new Trail()
            };

            return View(obj);
        }

       [HttpGet]
        public async Task<IActionResult> GetAllTrail()
        {
            return Json(new {data = await _trailRepository.GetAllAsync(SD.TrailAPIPath)});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _trailRepository.DeleteAsync(SD.TrailAPIPath, id);
            return Json(status ? new { success = true, message = "Delete Successful" } : new { success = false, message = "Delete Not Successful" });
            
        }

    }
}
