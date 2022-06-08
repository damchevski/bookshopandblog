using BSB.Data;
using BSB.Data.Entity;
using BSB.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSB.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<MAUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<MAUser>();
        }
        public void Delete(MAUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public MAUser Get(string id)
        {
            return entities
                 .Include(z => z.UserFavouriteMovies)
                 .Include("UserFavouriteMovies.MovieInUserFavourites")
                 .Include("UserFavouriteMovies.MovieInUserFavourites.Movie")
                 .SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<MAUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(MAUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(MAUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
