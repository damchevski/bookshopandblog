using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Service.Interface
{
    public interface IOrderService
    {
        Task<List<Order>> getAllOrders();
        Task<Order> getOrderDetails(Base entity);
    }
}
