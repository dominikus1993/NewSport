using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewSport.Domain.Entities;

namespace NewSport.Domain.Api
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }
        void Save(Comment comment);
        void Delete(int? id);
        void DeleteByPost(int? postId);
        int CountCommentsByPostId(Func<Comment, bool> func);
    }
}
