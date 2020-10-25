﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParkyAPI;
using ParkyApi.Data;
using ParkyApi.Migrations;
using ParkyApi.Models;
using ParkyApi.Repository.IRepository;

namespace ParkyApi.Repository.CRepository
{
    public class USerRepository : IUSer
    {

        private readonly ApplicationDbContext _applicationDBContext;
        private readonly AppSettings _appSettings;

        public USerRepository(ApplicationDbContext applicationDbContext, IOptions<AppSettings> appSettings)
        {
            _applicationDBContext = applicationDbContext;

            _appSettings = appSettings.Value;
        }

        public bool IsUnique(string UserName)
        {
            //var user = _applicationDBContext.Users.SingleOrDefault(s => s.Username == UserName);
            //return user switch
            //{
            //    null => true,
            //    _ => false
            //};

            return (_applicationDBContext.Users.Any(x => x.Username == UserName));
        }

        public User Authenticate(string username, string password)
        {
            var user = _applicationDBContext.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            //user not found
            if (user == null)
            {
                return null;
            }

            //if user was found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = ""; //Don't send password back to front

            return user;
        }

        public User Register(string username, string password)
        {
            var userObj = new User()
            {
                Username = username,
                Password = password,
                Role = "Admin"
            };
            _applicationDBContext.Add(userObj);
            _applicationDBContext.SaveChanges();
            userObj.Password = "";
            return userObj;
        }
    }


}
