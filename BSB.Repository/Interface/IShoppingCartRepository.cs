using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Repository.Interface
{
    public interface IShoppingCartRepository
    {
        void Update(ShoppingCart entity);
    }
}
