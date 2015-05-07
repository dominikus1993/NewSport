using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewSport.Domain.Entity;

namespace NewSport.WebApi.Models
{
    public class PostViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public String CurrentUser { get; set; }
        public Dictionary<int,int> CommentsCountByPostId { get; set; }
    }
}
