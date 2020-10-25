using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyApi.Migrations;
using ParkyApi.Models;

namespace ParkyApi.Repository.IRepository
{
    public interface IUSer
    {
        bool IsUnique(string UserName);
        User Authenticate(string username, string password);
        User Register(string username, string password);

    }
}
