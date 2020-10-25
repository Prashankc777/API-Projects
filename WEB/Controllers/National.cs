using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkyWeb;
using WEB.IRepositorys;
using WEB.Models;

namespace WEB.Controllers
{
    public class National : Controller
    {
        private readonly INationalRepository _nationalRepository;

        public National(INationalRepository nationalRepository)
        {
            _nationalRepository = nationalRepository;
        }
        public IActionResult Index()
        {
            return View(new NationalPark() { });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            NationalPark obj = new NationalPark();
            if (id is null)
            {
                return View(obj);
            }
            obj = await _nationalRepository.GetAsync(SD.NationalParkAPIPath, id.GetValueOrDefault());
            if (obj is null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public async Task<IActionResult> GelAllNationalPark()
        {
            return Json(new {data = await _nationalRepository.GetAllAsync(SD.NationalParkAPIPath)});
        }


        [HttpPost]
       
        public async Task<IActionResult> Upsert(NationalPark obj)
        {
            if (!ModelState.IsValid) return View(obj);
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                byte[] p1 = null;
                await using var fs1 = files[0].OpenReadStream();
                await using var ms1 = new MemoryStream();
                fs1.CopyTo(ms1);
                p1 = ms1.ToArray();

                obj.Picture = p1;
            }
            else
            {
                var objFromDb = await _nationalRepository.GetAsync(SD.NationalParkAPIPath, obj.Id);
                obj.Picture = objFromDb.Picture;
            }
            if (obj.Id == 0)
            {
                await _nationalRepository.CreateAsync(SD.NationalParkAPIPath, obj);
            }
            else
            {
                await _nationalRepository.UpdateAsync(SD.NationalParkAPIPath + obj.Id, obj);
            }
            return RedirectToAction(nameof(Index));


        }



        public async Task<IActionResult> Delete(int id)
        {
            var status = await _nationalRepository.DeleteAsync(SD.NationalParkAPIPath, id);
            return Json(status ? new { success = true, message = "Delete Successful" } : new { success = false, message = "Delete Not Successful" });
        }
    }
}
