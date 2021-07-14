using BSB.Data.Entity;
using BSB.Service.Interface;
using GemBox.Document;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSB.Web.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderService _orderService;
        private readonly UserManager<BSBUser> userManager;


        public OrderController(IOrderService orderService, UserManager<BSBUser> userManager)
        {
            this._orderService = orderService;
            this.userManager = userManager;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
       

        public async Task<IActionResult> Details(Guid id)
        {
            //HttpClient client = new HttpClient();


            //string URI = "https://localhost:5001/api/Admin/GetDetailsForOrder";

            //var model = new
            //{
            //    Id = id
            //};

            //HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            //HttpResponseMessage responseMessage = client.PostAsync(URI, content).Result;


            //var result = responseMessage.Content.ReadAsAsync<Order>().Result;

            Base baseEntity = new Base();
            baseEntity.Id = id;

            Order result = await this._orderService.getOrderDetails(baseEntity);


            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            BSBUser user = await this.userManager.GetUserAsync(User);

            List<Order> orders = await this._orderService.getAllOrders();

            List<Order> userOrders = new List<Order>();

            foreach (var item in orders)
            {
                if (item.UserId.Equals(user.Id))
                {
                    userOrders.Add(item);
                }
            }

            return View(userOrders);
        }
    }

}
    

