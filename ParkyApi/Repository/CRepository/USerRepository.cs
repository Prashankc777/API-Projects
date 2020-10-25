using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyApi.Data;
using ParkyApi.Migrations;
using ParkyApi.Repository.IRepository;

namespace ParkyApi.Repository.CRepository
{
    public class USerRepository : IUSer
    {
        private readonly ApplicationDbContext _dbContext;

        public USerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsUnique(string UserName)
        {
            throw new NotImplementedException();
        }

        public user Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public user Register(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
