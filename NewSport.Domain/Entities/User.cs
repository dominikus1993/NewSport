using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using NewSport.Domain.Entity;

namespace NewSport.Domain.Entities
{
    public class User
    {
        [HiddenInput(DisplayValue = false)]
        public Int32 Id { get; set; }

        [Required(ErrorMessage = "Nazwa użytkownia jest wymagana")]
        public String Username { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public byte Avatar { get; set; }

        [HiddenInput(DisplayValue = false)]
        public String AvatarMimeType { get; set; }

        //public virtual List<Post> Posts { get; set; }

       // public virtual List<Comment> Comments { get; set; }
    }
}
