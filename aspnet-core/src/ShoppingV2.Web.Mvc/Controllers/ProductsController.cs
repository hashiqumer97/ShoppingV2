using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingV2.Controllers;
using ShoppingV2.ServiceInterface;
using ShoppingV2.Web.Models.AddCart;

namespace ShoppingV2.Web.Controllers
{
    public class ProductsController : ShoppingV2ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var viewProducts = new AddCartViewModel();
            return View(viewProducts);
        }
        [HttpGet]
        public JsonResult GetProductById(int id)
        {
            var getProductById = _productService.GetProductById(id);
            return Json(getProductById);
        }
    }
}