using BSB.Data;
using BSB.Data.Entity;
using BSB.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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

        public List<Order> getAllOrders()
        {
            return entities
                .Include(z => z.User)
                .Include(z => z.ProductInOrders)
                .Include("ProductInOrders.Product")
                .ToListAsync().Result;
        }

        public Order getOrderDetails(Base entity)
        {
            return entities
               .Include(z => z.User)
               .Include(z => z.ProductInOrders)
               .Include("ProductInOrders.OrderedProduct")
               .SingleOrDefaultAsync(z => z.Id == entity.Id).Result;
        }
    }
}
