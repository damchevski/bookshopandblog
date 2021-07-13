using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Repository.Interface
{
    public interface IProductRepository
    {
    
        Task<List<Product>> GetAll();
        Task<List<Product>> GetAllRent();
        Task<List<Product>> GetAllBuy();
        Task<Product> GetProduct(Guid id);
        Task<Product> AddProduct(Product p);
        Task<Product> EditProduct(Product p);
        Task<Product> DeleteProduct(Guid id);
        Task<List<Product>> GroupByGenres(string Genre);
        Task<List<string>> GetAllGenres();

        void Insert(ProductInShoppingCart entity);

        void Insert(Order entity);

        void Insert(EmailMessage entity);

        void Insert(ProductInOrder entity);
        void Update(ShoppingCart entity);
    }
}
