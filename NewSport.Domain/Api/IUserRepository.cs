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
        void Save(User user);
        bool LogIn(string username,string password);
        User FindById(int? id);
        User FindByUsername(string username);
    }
}
