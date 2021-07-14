using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<List<Order>> getAllOrders();

        Task<Order> getOrderDetails(Base entity);
        void Insert(Order entity);

    }
}
