using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Service.Interface
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllCommentsForPost();
        Task<Comment> GetComment(Guid? id);
        Task<Comment> AddComment(Comment Comment);
        Task<Comment> EditComment(Comment Comment);
        Task<Comment> DeleteComment(Guid? id);

    }
}
