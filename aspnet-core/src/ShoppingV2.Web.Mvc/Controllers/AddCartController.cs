using System;
using System.Collections.Generic;
using System.Linq;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.ObjectMapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingV2.Application.BusinessObjects;
using ShoppingV2.BusinessObjects;
using ShoppingV2.Controllers;
using ShoppingV2.ServiceInterface;
using ShoppingV2.Web.Models.AddCart;
using ShoppingV2.Web.Models.Customer;
using ShoppingV2.Web.Models.Order;
using ShoppingV2.Web.Models.Product;

namespace ShoppingV2.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AddCartController : ShoppingV2ControllerBase
    {
        private readonly IProductService productService;
        private readonly ICustomerService customerService;
        private readonly IOrderService orderService;
        private readonly IObjectMapper objectMapper;
        public AddCartController(IProductService productService
            , ICustomerService customerService, IOrderService orderService, IObjectMapper objectMapper)
        {
            this.productService = productService;
            this.customerService = customerService;
            this.orderService = orderService;
            this.objectMapper = objectMapper;
        }
        [HttpGet]
        public IActionResult AddCart()
        {
            AddCartViewModel model = new AddCartViewModel
            {
                ProductListModel = new List<SelectListItem>()
            };
            var getCustomers = customerService.GetCustomers().ToList();
            model.SelectedCustomerName = objectMapper.Map<List<CustomerViewModel>>(getCustomers);
            var getProducts = productService.GetProducts().ToList();
            model.SelectedProductName = objectMapper.Map<List<ProductViewModel>>(getProducts);
            return View(model);
        }
    }
}