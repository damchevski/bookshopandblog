using BSB.Data.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteProductFromShoppingCart(string userId, Guid id);

        bool orderNow(string userId);

    }
}
