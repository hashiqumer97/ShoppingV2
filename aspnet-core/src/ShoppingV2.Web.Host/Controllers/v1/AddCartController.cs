using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.ObjectMapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingV2.BusinessObjects;
using ShoppingV2.Controllers;
using ShoppingV2.ServiceInterface;
using ShoppingV2.Web.Models.AddCart;
using ShoppingV2.Web.Models.Customer;
using ShoppingV2.Web.Models.Order;
using ShoppingV2.Web.Models.Product;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingV2.Web.Host.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
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

        [HttpGet("ViewCart")]
        public IActionResult ViewCart()
        {
            AddCartViewModel model1 = new AddCartViewModel
            {
                ProductListModel = new List<SelectListItem>()
            };
            var customers = customerService.GetCustomers().ToList();
            model1.SelectedCustomerName = objectMapper.Map<List<CustomerViewModel>>(customers);
            var products = productService.GetProducts().ToList();
            model1.SelectedProductName = objectMapper.Map<List<ProductViewModel>>(products);


            return View(model1);

        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody]OrdersViewModel ordersViewModel)
        {
            try
            {
                var order = objectMapper.Map<OrderBL>(ordersViewModel);
                orderService.CreateOrder(order);
                return RedirectToAction("AddCart", "AddCart");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
