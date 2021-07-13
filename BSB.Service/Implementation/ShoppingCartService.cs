using BSB.Data.Dto;
using BSB.Data.Entity;
using BSB.Repository.Interface;
using BSB.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSB.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IProductRepository productRepository;
        private readonly IUserRepository userRepository;

        public ShoppingCartService(IProductRepository productRepository, IUserRepository userRepository)
        {
            this.productRepository = productRepository;
            this.userRepository = userRepository;
        }
        public bool deleteProductFromShoppingCart(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this.userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.ProductInShoppingCarts.Where(z => z.ProductId.Equals(id)).FirstOrDefault();

                userShoppingCart.ProductInShoppingCarts.Remove(itemToDelete);

                this.productRepository.Update(userShoppingCart);

                return true;
            }

            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = this.userRepository.Get(userId);

            var userShoppingCart = loggedInUser.UserCart;

            var AllProducts = userShoppingCart.ProductInShoppingCarts.ToList();

            var allProductPrice = AllProducts.Select(z => new
            {
                ProductPrice = z.Product.Price,
                Quanitity = z.Quantity
            }).ToList();

            var totalPrice = 0.0;


            foreach (var item in allProductPrice)
            {
                totalPrice += item.Quanitity * item.ProductPrice;
            }


            ShoppingCartDto scDto = new ShoppingCartDto
            {
                Products = AllProducts,
                TotalPrice = totalPrice
            };


            return scDto;

        }

        public bool orderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this.userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                EmailMessage mail = new EmailMessage();
                mail.MailTo = loggedInUser.Email;
                mail.Subject = "Successfully created order";
                mail.Status = false;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this.productRepository.Insert(order);

                List<ProductInOrder> productInOrders = new List<ProductInOrder>();

                var result = userShoppingCart.ProductInShoppingCarts.Select(z => new ProductInOrder
                {
                    Id = Guid.NewGuid(),
                    ProductId = z.Product.Id,
                    Product = z.Product,
                    OrderId = order.Id,
                    Order = order
                }).ToList();

                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order conains: ");

                for (int i = 1; i <= result.Count(); i++)
                {
                    var item = result[i - 1];

                    totalPrice += item.Quantity * item.Product.Price;

                    sb.AppendLine(i.ToString() + ". " + item.Product.Name + " with price of: " + item.Product.Price + " and quantity of: " + item.Quantity);
                }

                sb.AppendLine("Total price: " + totalPrice.ToString());


                mail.Content = sb.ToString();


                productInOrders.AddRange(result);

                foreach (var item in productInOrders)
                {
                    this.productRepository.Insert(item);
                }

                loggedInUser.UserCart.ProductInShoppingCarts.Clear();

                this.userRepository.Update(loggedInUser);
                this.productRepository.Insert(mail);

                return true;
            }
            return false;
        }
    }

        

}
