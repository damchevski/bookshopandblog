using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BSB.Data.Entity
{
    public class Product : Base
    {
        [Required]
        [Display(Name ="Product Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Product Image")]
        public string Image { get; set; }
        [Required]
        [Display(Name = "Product Price")]
        public float Price { get; set; }
        [Required]
        [Display(Name = "Product for Buy ?")]
        public bool IsForBuy { get; set; }
        [Required]
        [Display(Name = "Book Genre")]
        public string Genre { get; set; }
        public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public IEnumerable<ProductInOrder> ProductInOrders { get; set; }
    }
}
