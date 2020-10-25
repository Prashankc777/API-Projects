using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyApi.Migrations;

namespace ParkyApi.Repository.IRepository
{
    public interface IUSer
    {
        bool IsUnique(string UserName);
        user Authenticate(string username, string password);
        user Register(string username, string password);

    }
}
