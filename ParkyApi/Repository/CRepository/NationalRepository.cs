using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyApi.Data;
using ParkyApi.Models;

namespace ParkyApi.Repository
{
    public class NationalRepository :INationalRepository
    {
        private readonly ApplicationDbContext _db;
        public NationalRepository(ApplicationDbContext db)
        {
            _db = db;

        }
        public ICollection<NationalPark> GetNationalParks()
        {
            return _db.NationalParks.OrderBy(a => a.Id).ToList();
        }
        public NationalPark GetNationalPark(int NationalParkId)
        {
           return _db.NationalParks.FirstOrDefault(a => a.Id == NationalParkId);
        }
        public bool NationalParkExist(string Name)
        {
            var value = _db.NationalParks.Any(a => a.Name.ToLower().Trim() == Name.ToLower().Trim());
            return value;
        }

        public bool NationalParkExist(int id)
        {
            return _db.NationalParks.Any(a => a.Id == id);
        }

        public bool CreateNationalPark(NationalPark NationalPark) 
        {
            _db.NationalParks.Add(NationalPark);
            return Save();
        }

        public bool UpdateNationalPark(NationalPark NationalPark)
        {
            _db.NationalParks.Update(NationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark NationalPark)
        {
            _db.NationalParks.Remove(NationalPark);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
