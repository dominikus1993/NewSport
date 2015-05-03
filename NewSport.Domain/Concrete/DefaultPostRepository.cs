using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewSport.Domain.Api;
using NewSport.Domain.Entity;

namespace NewSport.Domain.Concrete
{
    public class DefaultPostRepository:IPostRepository
    {
        private readonly DefaultDbContext _dbContext;

        public DefaultPostRepository()
        {
            this._dbContext = new DefaultDbContext();
        }

        public IQueryable<Post> Posts
        {
            get { return _dbContext.Posts; }
        }

        public void Save(Post post)
        {
            if (post.Id == 0)
            {
                _dbContext.Posts.Add(post);
            }
            else
            {
               Edit(post);
            }
            _dbContext.SaveChanges();
        }

        public void Delete(Post post)
        {
            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();
        }

        public Post FindById(int? id)
        {
            return _dbContext.Posts.Include(x=>x.Author).FirstOrDefault(x=>x.Id == id);
        }

        public IQueryable<Post> FindByUser(string username)
        {
            var posts = _dbContext.Posts.Include(u=>u.Author);
            return posts.Where(x => x.Author.Username == username);
        }

        private void Edit(Post post)
        {
            Post editingPost = FindById(post.Id);
            if (editingPost != null)
            {
                editingPost.Text = post.Text;
                editingPost.Title = post.Title;
            }
        }
    }
}
