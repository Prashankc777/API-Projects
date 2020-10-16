 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 using ParkyApi.Models;

 namespace ParkyApi.Repository
{
    public interface INationalRepository
    {
        ICollection<NationalPark> GetNationalParks();

        NationalPark GetNationalPark(int NationalParkId);

        bool NationalParkExist(string Name);
        bool NationalParkExist(int id);

        bool CreateNationalPark(NationalPark NationalPark);

        bool UpdateNationalPark(NationalPark NationalPark);
        bool DeleteNationalPark(NationalPark NationalPark);

        bool Save();
    }
}
