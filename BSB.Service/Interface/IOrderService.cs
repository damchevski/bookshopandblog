using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Service.Interface
{
    public interface IOrderService
    {
        List<Order> getAllOrders();
        Order getOrderDetails(Base entity);
    }
}
