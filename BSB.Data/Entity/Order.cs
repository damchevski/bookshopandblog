using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Entity
{
    public class Order : Base
    {
        public string UserId { get; set; }
        public BSBUser User { get; set; }
        public ICollection<ProductInOrder> ProductInOrders { get; set; }
    }
}
