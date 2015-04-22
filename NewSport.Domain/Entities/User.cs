using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NewSport.Domain.Entity;

namespace NewSport.Domain.Entities
{
    public class User
    {
        [HiddenInput(DisplayValue = false)]
        public Int32 Id { get; set; }

        [Required]
        public String Username { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}
