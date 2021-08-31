using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Repository.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAll();
        Task<Comment> GetComment(Guid id);
        Task<Comment> AddComment(Comment p);
        Task<Comment> EditComment(Comment p);
        Task<Comment> DeleteComment(Guid id);
        void Insert(CommentInPost entity);
    }
}
