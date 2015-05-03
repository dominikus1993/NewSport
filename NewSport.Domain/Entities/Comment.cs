using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NewSport.Domain.Entity;

namespace NewSport.Domain.Entities
{
    public class Comment
    {
        public Comment()
        {
            CommentsDate = DateTime.Now;
        }

        [HiddenInput(DisplayValue = false)]
        public Int32 Id { get; set; }

        public String Message { get; set; }

        public DateTime CommentsDate { get; set; }

        public int? AuthorId { get; set; }

        public int? PostId { get; set; }

        [ForeignKey("AuthorId")]
        public User Author { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
