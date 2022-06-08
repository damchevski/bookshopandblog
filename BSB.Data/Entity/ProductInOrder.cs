using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Entity
{
    public class ProductInOrder :Base
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
