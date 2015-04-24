using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewSport.Domain.Entity;

namespace NewSport.Domain.Api
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get;}
        void Save(Post post);
        void Delete(Post post);
        Post FindById(int? id);
        IQueryable<Post> FindByUser(string username);
    }
}
