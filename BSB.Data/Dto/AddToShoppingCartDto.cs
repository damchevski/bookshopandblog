using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Dto
{
    public class AddToShoppingCartDto
    {
        public Product SelectedProduct { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

    }
}
