using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewSport.Domain.Entity;

namespace NewSport.Domain.Entities
{
    public class User
    {
        public Int32 Id { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
}
