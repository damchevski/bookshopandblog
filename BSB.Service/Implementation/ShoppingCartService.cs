using BSB.Data.Dto;
using BSB.Data.Entity;
using BSB.Repository.Interface;
using BSB.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace BSB.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IProductRepository productRepository;
        private readonly IUserRepository userRepository;
        private readonly IEmailService mailService;
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IEmailRepository emailRepository;
        private readonly IOrderRepository orderRepository;
        private readonly ITwilioRestClient smsClient;
        public ShoppingCartService(IProductRepository productRepository, 
            IUserRepository userRepository,
            IEmailRepository emailRepository, IEmailService emailService,
            IShoppingCartRepository shoppingCartRepository, IOrderRepository orderRepository,
            ITwilioRestClient smsClient)
        {
            this.smsClient = smsClient;
            this.orderRepository = orderRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.mailService = emailService;
            this.productRepository = productRepository;
            this.userRepository = userRepository;
            this.emailRepository = emailRepository;
        }
        public async Task<bool> deleteProductFromShoppingCart(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this.userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.ProductInShoppingCarts.Where(z => z.ProductId.Equals(id)).FirstOrDefault();

                userShoppingCart.ProductInShoppingCarts.Remove(itemToDelete);

                this.shoppingCartRepository.Update(userShoppingCart);

                return true;
            }

            return false;
        }

        public async Task<ShoppingCartDto> getShoppingCartInfo(string userId)
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

        public async Task<bool> orderNow(string userId)
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

                this.orderRepository.Insert(order);

                List<ProductInOrder> productInOrders = new List<ProductInOrder>();

                var result = userShoppingCart.ProductInShoppingCarts.Select(z => new ProductInOrder
                {
                    Id = Guid.NewGuid(),
                    ProductId = z.Product.Id,
                    Product = z.Product,
                    OrderId = order.Id,
                    Order = order,
                    Quantity = z.Quantity
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
                await this.emailRepository.Add(mail);

                List<EmailMessage> mess = new List<EmailMessage>();
                mess.Add(mail);

                await this.mailService.SendEmailAsync(mess);

                productInOrders.AddRange(result);

                foreach (var item in productInOrders)
                {
                    this.productRepository.Insert(item);
                }

                loggedInUser.UserCart.ProductInShoppingCarts.Clear();

                this.userRepository.Update(loggedInUser);

                var message = MessageResource.Create(
                to: new PhoneNumber(loggedInUser.PhoneNumber),
                from: new PhoneNumber("+13462144811"),
                body: "You make successfull order! Total Price: "+totalPrice ,
                client: smsClient); 

                return true;
            }
            return false;
        }
    }

        

}
