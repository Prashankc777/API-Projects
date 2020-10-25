using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkyApi.Data;
using ParkyApi.Models;
using ParkyApi.Repository.IRepository;

namespace ParkyApi.Repository.CRepository
{
    public class TrailRepository : IParkyRepository
    {
        private readonly ApplicationDbContext _db;

        public TrailRepository(ApplicationDbContext db )
        {
            _db = db;
        }


        public ICollection<Trail> GetTrail()
        {
            return _db.Trails.Include(c=>c.NationalPark).OrderBy(a=>a.Id).ToList();
        }

        public Trail GetTrail(int trailId)
        {
            return _db.Trails.Include(c => c.NationalPark).FirstOrDefault(a => a.Id == trailId);
        }

        public bool trialExist(string Name)
        {
            return _db.Trails.Any(a => a.Name.ToLower().Trim() == Name.ToLower().Trim());
        }

        public bool trialExist(int id)
        {
            return _db.Trails.Any(a => a.Id == id);
        }

        public bool CreateTrail(Trail trail)
        {
            _db.Trails.Add(trail);
            return Save();
        }

        public bool Updatetrail(Trail trail)
        {
            _db.Trails.Update(trail);
            return Save();
        }

        public bool Deletetrail(Trail trail)
        {
            _db.Trails.Remove(trail);
            return Save();
        }

        public bool Save()
        {
           return _db.SaveChanges() >= 0  ? true : false;
        }

        public ICollection<Trail> GetTrailsInNationalPark(int npl)
        {
            return _db.Trails.Include(c => c.NationalPark).Where(x => x.NationalParkId == npl).ToList();
        }
    }
}
