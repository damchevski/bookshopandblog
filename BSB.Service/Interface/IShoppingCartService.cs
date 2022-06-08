using BSB.Data.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Service.Interface
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> getShoppingCartInfo(string userId);
        Task<bool> deleteProductFromShoppingCart(string userId, Guid id);

        Task<bool> orderNow(string userId);

    }
}
