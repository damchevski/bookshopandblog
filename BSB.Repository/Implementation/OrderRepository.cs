using BSB.Data;
using BSB.Data.Entity;
using BSB.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errormessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }

        public async Task<List<Order>> getAllOrders()
        {
            return await entities
                .Include(z => z.User)
                .Include(z => z.ProductInOrders)
                .Include("ProductInOrders.Product")
                .ToListAsync();
        }

        public async Task<Order> getOrderDetails(Base entity)
        {
            return await entities
               .Include(z => z.User)
               .Include(z => z.ProductInOrders)
               .Include("ProductInOrders.Product")
               .SingleOrDefaultAsync(z => z.Id == entity.Id);
        }

        public void Insert(Order entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this.context.Add(entity);
            this.context.SaveChanges();
        }
    }
}
