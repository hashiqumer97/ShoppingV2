using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingV2.BusinessObjects;
using ShoppingV2.Controllers;
using ShoppingV2.ServiceInterface;
using ShoppingV2.Web.Models.Order;
using Abp.ObjectMapping;

namespace ShoppingV2.Web.Controllers
{
    public class OrdersController : ShoppingV2ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly Abp.ObjectMapping.IObjectMapper objectMapper;

        public OrdersController(IOrderService orderService, Abp.ObjectMapping.IObjectMapper objectMapper)
        {
            this.orderService = orderService;
            this.objectMapper = objectMapper;
        }
        [HttpGet]
        [AbpMvcAuthorize]
        public IActionResult Orders()
        {
            var model = ObjectMapper.Map<IEnumerable<OrdersViewModel>>(orderService.GetOrders());
            return View(model);
        }

        public IActionResult GetOrdersById(int id)
        {
            var result = orderService.GetOrderById(id);
            return Json(result);
        }
        [HttpGet]
        [AbpMvcAuthorize]
        public IActionResult OrderItems(int ordid)
        {
            var model = orderService.GetOrderById(ordid);
            var obj = ObjectMapper.Map<OrdersViewModel>(model);
            return View(obj);
        }

        [HttpPost]
        public IActionResult EditOrders([FromBody]OrdersViewModel ordersViewModel)
        {
            var order = objectMapper.Map<OrderBL>(ordersViewModel);
            orderService.ChangeOrder(order);
            return RedirectToAction("AddCart", "AddCart");
        }

        [HttpPost]
        public IActionResult DeleteOrder([FromBody]OrdersViewModel ordersViewModel)
        {
            var order = objectMapper.Map<OrderBL>(ordersViewModel);
            orderService.ChangeOrder(order);
            return RedirectToAction("AddCart", "AddCart");
        }

        [HttpPost]
        public IActionResult DeleteEntireOrder([FromBody]OrdersViewModel ordersViewModel)
        {

            var order = objectMapper.Map<OrderBL>(ordersViewModel);
            orderService.DeleteEntireOrder(order);
            return View(order);

        }



    }

}
