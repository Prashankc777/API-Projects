using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ParkyWeb.Models;
using ParkyWeb.Repository.Interface;

using ParkyWeb.Repository.IRepositorys;

namespace ParkyWeb.Repository
{
    public class NationalParkRepository : Repository<NationalPArk>, INationalParkRepository
    {
        private readonly IHttpClientFactory _clineFactory;

        public NationalParkRepository(IHttpClientFactory clineFactory) :base(clineFactory)
        {
            _clineFactory = clineFactory;
        }
    }
}
