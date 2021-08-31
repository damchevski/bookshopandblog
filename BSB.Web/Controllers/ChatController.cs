using BSB.Web.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BSB.Web.Controllers
{
    public class ChatController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            ChatHub.connectionMappings.Add(new Data.ConnectionMapping() { UserId = User.Identity.Name });

            return View("Index", ChatHub.connectionMappings) ;
        }
    }
}
