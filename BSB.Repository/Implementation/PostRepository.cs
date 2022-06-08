using BSB.Data;
using BSB.Data.Entity;
using BSB.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Repository.Implementation
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Post> _entities;

        public PostRepository(ApplicationDbContext context)
        {
            this._context = context;
            _entities = context.Set<Post>();
        }

        public async Task<Post> AddPost(Post p)
        {
            this._context.Add(p);
            await _context.SaveChangesAsync();

            return p;
        }

        public async Task<Post> DeletePost(Guid id)
        {
            var Post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(Post);
            await _context.SaveChangesAsync();

            return Post;
        }

        public async Task<Post> EditPost(Post p)
        {
            this._context.Update(p);
            await _context.SaveChangesAsync();

            return p;
        }


        public async Task<List<Post>> GetAll()
        {
            return await _entities
                 .Include(z => z.ByUser)
                 .Include(z => z.CommentsInPost)
                 .ToListAsync();
        }

        public async Task<List<Post>> GetAllForTopic(string topic)
        {
            return await _context.Posts.Where(x => x.Topic.Equals(topic)).ToListAsync();
        }

        public async Task<List<string>> GetAllTopics()
        {
            List<string> res = new List<string>();

            foreach (var post in await this.GetAll()) {
                if (!res.Contains(post.Topic))
                    res.Add(post.Topic);
            }

            return res;
        }
        
        public async Task<Post> GetPost(Guid id)
        {
            return await _entities.Where(z => z.Id.Equals(id))
                 .Include(z => z.ByUser)
                 .Include(z => z.CommentsInPost)
                 .Include("CommentsInPost.Comment")
                 .Include("CommentsInPost.Comment.ByUser")
                 .SingleOrDefaultAsync();
        }
    }
}
