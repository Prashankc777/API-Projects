using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WEB.IRepositorys;
using WEB.Models;

namespace WEB.Repo
{
    public class TrailRepository : Repository<Trail> , ITrailRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public TrailRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
