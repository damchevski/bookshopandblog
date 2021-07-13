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
        private DbSet<BSBUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<BSBUser>();
        }
        public void Delete(BSBUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public BSBUser Get(string id)
        {
            return entities
                 .Include(z => z.UserCart)
                 .Include("UserCart.ProductInShoppingCarts")
                 .Include("UserCart.ProductInShoppingCarts.Product")
                 .SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<BSBUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(BSBUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(BSBUser entity)
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
