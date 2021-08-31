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
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        private DbSet<Comment> _entities;

        public CommentRepository(ApplicationDbContext context)
        {
            this._context = context;
            _entities = context.Set<Comment>();
        }

        public async Task<Comment> AddComment(Comment p)
        {
            this._context.Add(p);
            await _context.SaveChangesAsync();

            return p;
        }

        public async Task<Comment> DeleteComment(Guid id)
        {
            var Comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(Comment);
            await _context.SaveChangesAsync();


            return Comment;
        }

        public async Task<Comment> EditComment(Comment p)
        {
            this._context.Update(p);
            await _context.SaveChangesAsync();

            return p;
        }


        public async Task<List<Comment>> GetAll()
        {
            return await _entities
                 .Include(z => z.ByUser)
                 .Include(z => z.CommentInPost)
                 .Include(z => z.CommentInPost.Post)
                 .ToListAsync();
        }

        public async Task<Comment> GetComment(Guid id)
        {
            return await _context.Comments
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Insert(CommentInPost entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");  
            }
            _context.Add(entity);
            _context.SaveChanges();
        }
    }
}
