using BSB.Data.Entity;
using BSB.Repository.Interface;
using BSB.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        
        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;

        }
        public async Task<List<Order>> getAllOrders()
        {
            return await this.orderRepository.getAllOrders();
        }

        public async Task<Order> getOrderDetails(Base entity)
        {
            return await this.orderRepository.getOrderDetails(entity);
        }
    }
}
