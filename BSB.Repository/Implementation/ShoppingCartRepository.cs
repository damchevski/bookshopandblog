using BSB.Data;
using BSB.Data.Entity;
using BSB.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Repository.Implementation
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public void Update(ShoppingCart entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this._context.Update(entity);
            this._context.SaveChanges();
        }
    }
}
