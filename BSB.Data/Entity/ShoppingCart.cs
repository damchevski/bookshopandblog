using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Entity
{
    public class ShoppingCart : Base
    {
        public string UserId { get; set; }
        public BSBUser User { get; set; }
        public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
    }
}
