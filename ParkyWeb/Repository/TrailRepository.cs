using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ParkyWeb.Models;
using ParkyWeb.Repository.IRepositorys;

namespace ParkyWeb.Repository
{
    public class TrailRepository : Repository<Trail>, IRepository<Trail>
    {
        private readonly IHttpClientFactory _clineFactory;

        public TrailRepository(IHttpClientFactory clineFactory) : base(clineFactory)
        {
            _clineFactory = clineFactory;

        }
    }
}




