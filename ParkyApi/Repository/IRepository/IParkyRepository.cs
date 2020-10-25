using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyApi.Models;
using ParkyApi.Repository.CRepository;

namespace ParkyApi.Repository.IRepository
{
    public interface IParkyRepository
    {
        ICollection<Trail> GetTrail();
    
        Trail GetTrail(int trailId);

        bool trialExist(string Name);
        bool trialExist(int id);

        bool CreateTrail(Trail trail);

        bool Updatetrail(Trail trail);
        bool Deletetrail(Trail trail);

        bool Save();
        ICollection<Trail> GetTrailsInNationalPark(int npl);

    }
}
