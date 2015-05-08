using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewSport.Domain.Entities;
using NewSport.Domain.Entity;

namespace NewSport.Domain.Api
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get;}
        IQueryable<Role> Roles { get; }
        void Save(User user);
        bool LogIn(string username,string password);
        void LogOff();
        bool IsLogged();
        User FindById(int? id);
        User FindByUsername(string username); 
        void Delete(User user);
    }
}
