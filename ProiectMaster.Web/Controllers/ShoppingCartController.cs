using Microsoft.AspNetCore.Mvc;
using ProiectMaster.Models.DTOs.VM;
using ProiectMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectMaster.Web.Controllers
{
    [Route("cart")]
    public class ShoppingCartController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            var cart = SessionHelper.Get<List<ProductVM>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            return View();
        }
    }
}
