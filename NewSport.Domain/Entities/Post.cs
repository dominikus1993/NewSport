using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NewSport.Domain.Entities;

namespace NewSport.Domain.Entity
{
    public class Post
    {
        public Post()
        {
            Date = DateTime.Now;
        }

        [HiddenInput(DisplayValue = false)]
        public Int32 Id { get; set; }
       
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Title is required")]
        public String Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Text is required")]
        public String Text { get; set; }

        public DateTime Date { get; private set; }

        public int? AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public User Author { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public override string ToString()
        {
            return "Id = " + Id+ " Title = " + Title + " Text = " + Text + " Date = " + Date;
        }
    }
}
