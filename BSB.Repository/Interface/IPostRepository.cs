using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Repository.Interface
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAll();
        Task<List<Post>> GetAllForTopic(string topic);
        Task<Post> GetPost(Guid id);
        Task<Post> AddPost(Post p);
        Task<Post> EditPost(Post p);
        Task<Post> DeletePost(Guid id);
        Task<List<string>> GetAllTopics();
    }
}
