using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Entity
{
    public class ProductInShoppingCart : Base
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
