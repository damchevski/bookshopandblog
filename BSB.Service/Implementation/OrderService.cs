using BSB.Data.Entity;
using BSB.Repository.Interface;
using BSB.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        
        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;

        }
        public List<Order> getAllOrders()
        {
            return this.orderRepository.getAllOrders();
        }

        public Order getOrderDetails(Base entity)
        {
            return this.orderRepository.getOrderDetails(entity);
        }
    }
}
