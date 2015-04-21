using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewSport.Domain.Api;
using NewSport.Domain.Entities;

namespace NewSport.Domain.Concrete
{
    public class DefaultUserRepository:IUserRepository
    {
        private DefaultDbContext _dbContext;
        public IQueryable<User> Users
        {
            get { return _dbContext.Users; }
        }

        public void Save(User user)
        {
            throw new NotImplementedException();
        }
    }
}
