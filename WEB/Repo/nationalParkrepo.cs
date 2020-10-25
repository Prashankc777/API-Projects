using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WEB.IRepositorys;
using WEB.Models;

namespace WEB.Repo
{
    public class nationalParkrepo : Repository<NationalPark>, INationalRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public nationalParkrepo(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
