﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.UI.WebControls;
using NewSport.Domain.Api;
using NewSport.Domain.Entities;

namespace NewSport.Domain.Concrete
{
    public class DefaultUserRepository:IUserRepository
    {
        private readonly DefaultDbContext _dbContext;

        public DefaultUserRepository()
        {
            this._dbContext = new DefaultDbContext();
        }

        public IQueryable<User> Users
        {
            get { return _dbContext.Users; }
        }

        public void Save(User user)
        {
            MD5 encrypter = MD5.Create();
            if (user.Id == 0)
            {
                user.Password = encrypter.ComputeHash(Encoding.Default.GetBytes(user.Password)).ToString();
                _dbContext.Users.Add(user);
            }
            else
            {
                EditUser(user);
            }
        }

        private void EditUser(User user)
        {
            User editingUser = _dbContext.Users.FirstOrDefault(x => x.Id == user.Id);
            if (editingUser != null)
            {
                editingUser.Username = user.Username;
                editingUser.Email = user.Email;
            }
        }

        public bool LogIn(string username, string password)
        {
            bool loginResult = Users.Any(x => x.Username == username && x.Password == password);
            if (loginResult)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return loginResult;
        }

        public User FindById(int? id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public User FindByUsername(string username)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Username == username);
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
