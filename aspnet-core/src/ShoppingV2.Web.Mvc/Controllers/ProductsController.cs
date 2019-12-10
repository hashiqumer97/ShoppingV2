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
            var model = new AddCartViewModel();

            return View(model);
        }
        [HttpGet]
        public JsonResult GetProductById(int id)
        {
            var result = _productService.GetProductSubCategories(id);
            return Json(result);

        }



    }


}