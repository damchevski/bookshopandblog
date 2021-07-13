using BSB.Data.Dto;
using BSB.Data.Entity;
using BSB.Repository.Interface;
using BSB.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository userRepository;
        private readonly ILogger<ProductService> logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger, IUserRepository userRepository)
        {
            this._productRepository = productRepository;
            this.logger = logger;
            this.userRepository = userRepository;
        }

        public async Task<Product> AddProduct(Product product)
        {
            if (product.Id == null ||
                product.Image == null ||
                product.IsForBuy == null || product.Price == null)
                return null;

            return await this._productRepository.AddProduct(product);
        }

        public async Task<bool> AddToShoppingCart(AddToShoppingCartDto item, string userID)
        {
            var user = this.userRepository.Get(userID);
            var userShoppingCart = user.UserCart;

            if (item.ProductId != null && userShoppingCart != null)
            {
                var product = await this._productRepository.GetProduct(item.ProductId);

                if (product != null)
                {
                    foreach (var book in userShoppingCart.ProductInShoppingCarts) {
                        if (book.Product.IsForBuy != product.IsForBuy) {
                            return false;
                        }
                    }

                    ProductInShoppingCart itemToAdd = new ProductInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        Product = product,
                        ProductId = product.Id,
                        ShoppingCart = userShoppingCart,
                        ShoppingCartId = userShoppingCart.Id,
                        Quantity = item.Quantity
                    };
                    this._productRepository.Insert(itemToAdd);
                    logger.LogInformation("Product was succesfully added into ShoppingCart");
                    return true;
                }
                return false;
            }
            logger.LogInformation("Something was wrong. ProductId or UserShoppingCard may be unaveliable!");
            return false;
        }

        public async Task<Product> DeleteProduct(Guid? id)
        {
            if (id == null)
                return null;

            return await this._productRepository.DeleteProduct(id.Value);
        }

        public async Task<Product> EditProduct(Product product)
        {
            if (product.Id == null)
                return null;

            return await this._productRepository.EditProduct(product);
        }

        public async Task<List<string>> GetAllGenres()
        {
            return await this._productRepository.GetAllGenres();
        }

        public  async Task<List<Product>> GetAllProducts(string? SearchString, bool? rent)
        {
            var result = new List<Product>();

            if (rent == null)
                result= await this._productRepository.GetAll();
            else if (rent == true)
                result= await this._productRepository.GetAllBuy();
            else
                result= await this._productRepository.GetAllRent();

            if (SearchString == null || SearchString.Equals(""))
                return result;

            var forReturn = new List<Product>();
            foreach(var book in result)
            {
                if (book.Name.ToLower().Contains(SearchString.ToLower()))
                    forReturn.Add(book);
            }

            return forReturn;
        }

        public async Task<Product> GetProduct(Guid? id)
        {
            if (id == null)
                return null;

            return await this._productRepository.GetProduct(id.Value);
        }

        public async Task<AddToShoppingCartDto> GetShoppingCartInfo(Guid? id)
        {
            var product = await this.GetProduct(id);
            AddToShoppingCartDto model = new AddToShoppingCartDto
            {
                SelectedProduct = product,
                ProductId = product.Id,
                Quantity = 1
            };
            return model;
        }

        public async Task<List<Product>> GroupByGenres(string? Genre)
        {
            if (Genre == null || Genre.Equals(""))
                return await this._productRepository.GetAll();

            return await this._productRepository.GroupByGenres(Genre);
        }
    }
}
