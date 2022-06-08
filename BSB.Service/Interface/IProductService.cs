using BSB.Data.Dto;
using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Service.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts(string? SearchString, bool? rent);
        Task<Product> GetProduct(Guid? id);
        Task<Product> AddProduct(Product product);
        Task<Product> EditProduct(Product product);
        Task<Product> DeleteProduct(Guid ?id);
        Task<List<Product>> GroupByGenres(string? Genre);
        Task<List<string>> GetAllGenres();

        Task<AddToShoppingCartDto> GetShoppingCartInfo(Guid? id);

        Task<bool> AddToShoppingCart(AddToShoppingCartDto item, string userID);
    }
}
