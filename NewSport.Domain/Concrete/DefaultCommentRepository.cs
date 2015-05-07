using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using NewSport.Domain.Api;
using NewSport.Domain.Entities;

namespace NewSport.Domain.Concrete
{
    public class DefaultCommentRepository:ICommentRepository
    {
        private readonly DefaultDbContext _dbContext;

        public DefaultCommentRepository()
        {
            _dbContext = new DefaultDbContext();
        }

        public IQueryable<Comment> Comments
        {
            get { return _dbContext.Comments.Include(x=>x.Author); }
        }

        public void Save(Comment comment)
        {
            if (comment.Id == 0)
            {
                _dbContext.Comments.Add(comment);
            }
            _dbContext.SaveChanges();
        }

        public void Delete(int? id)
        {
            Comment commentToDelete = _dbContext.Comments.First(x => x.Id == id);
            if (commentToDelete != null)
            {
                _dbContext.Comments.Remove(commentToDelete);
            }
        }

        public void DeleteByPost(int? postId)
        {
            IEnumerable<Comment> commentsToDelete = _dbContext.Comments.Where(x => x.PostId == postId);
            if (commentsToDelete.Any())
            {
                _dbContext.Comments.RemoveRange(commentsToDelete);
            }
        }

        public int CountCommentsByPostId(Func<Comment, bool> func)
        {
            return _dbContext.Comments.Count(func);
        }
    }
}
