using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Service.Interface
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPosts(string? topic);
        Task<Post> GetPost(Guid? id);
        Task<Post> AddPost(Post Post, string userId);
        Task<Post> EditPost(Post Post);
        Task<Post> DeletePost(Guid? id);
        Task<List<string>> GetTopics();
        Task<Post> Like(Guid postId, string UserEmail);
        Task<Post> Unlike(Guid postId, string UserEmail);
        Task<CommentInPost> CommentOnPost(string commentContent, Guid postId, string userId);
    }
}
