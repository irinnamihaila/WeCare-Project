using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectMaster.Models.Interfaces;
using ProiectMaster.Web.Models;

namespace ProiectMaster.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = productService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            var product = productService.GetProduct(id);
            return View(product);
        }

        [HttpPost]
        [Route("Add/{id}")]
        public IActionResult Add(int id)
        {
            var shopList = HttpContext.Session.Get<List<int>>(SessionHelper.ShoppingCart);

            if (shopList == null)
                shopList = new List<int>();

            if (!shopList.Contains(id))
                shopList.Add(id);

            HttpContext.Session.Set(SessionHelper.ShoppingCart, shopList);

            return RedirectToAction("Index", "Home", productService.GetAllProducts());
        }

        [HttpPost]
        [Route("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var shopList = HttpContext.Session.Get<List<int>>(SessionHelper.ShoppingCart);

            if (shopList == null)
                return RedirectToAction("Index", "Home", productService.GetAllProducts());

            if (shopList.Contains(id))
                shopList.Remove(id);

            HttpContext.Session.Set(SessionHelper.ShoppingCart, shopList);

            return RedirectToAction("Index", "Home", productService.GetAllProducts());
        }
    }
}

