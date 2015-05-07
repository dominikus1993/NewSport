using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewSport.Domain.Entities;
using NewSport.Domain.Entity;

namespace NewSport.Domain.Concrete
{
    public class DefaultDbContext:DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
