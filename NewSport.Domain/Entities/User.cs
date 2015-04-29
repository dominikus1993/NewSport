using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NewSport.Domain.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NewSport.Domain.Entities
{
    public class User:IdentityUser
    {
        public virtual List<Post> Posts { get; set; }
    }
}
