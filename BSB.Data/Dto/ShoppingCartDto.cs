using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Dto
{
    public class ShoppingCartDto
    {
        public List<ProductInShoppingCart> Products { get; set; }

        public double TotalPrice { get; set; }
    }
}
