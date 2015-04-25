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

namespace NewSport.Domain.Entities
{
    public class User
    {
        [HiddenInput(DisplayValue = false)]
        public Int32 Id { get; set; }

        [Required(ErrorMessage = "Nazwa użytkownia jest wymagana")]
        [Index(IsUnique = true)]
        public String Username { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [Index(IsUnique = true)]
        public String Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}
