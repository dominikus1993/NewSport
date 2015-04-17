using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewSport.WebApi.Models
{
    public class PostViewModel
    {
        public Int32 TotalPosts { get; set; }
        public Int32 PostsPerPage { get; set; }
        public Int32 CurrentPage { get; set; }

        public Int32 TotalPages()
        {
            return (int) Math.Ceiling((decimal) (TotalPosts/PostsPerPage));
        }
    }
}