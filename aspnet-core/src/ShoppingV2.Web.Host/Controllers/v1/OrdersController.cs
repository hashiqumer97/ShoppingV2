﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingV2.BusinessObjects;
using ShoppingV2.Controllers;
using ShoppingV2.ServiceInterface;
using ShoppingV2.Web.Models.Order;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingV2.Web.Host.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ShoppingV2ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly Abp.ObjectMapping.IObjectMapper objectMapper;

        public OrdersController(IOrderService orderService, Abp.ObjectMapping.IObjectMapper objectMapper)
        {
            this.orderService = orderService;
            this.objectMapper = objectMapper;
        }
        [HttpGet("{orders}")]
        [AbpMvcAuthorize]
        public IActionResult Orders()
        {
            var model = ObjectMapper.Map<IEnumerable<OrdersViewModel>>(orderService.GetOrders());
            return View(model);
        }
        [HttpGet("{Getordersbyid}")]
        public IActionResult GetOrdersById(int id)
        {
            var result = orderService.GetOrderById(id);
            return Json(result);
        }
        [HttpGet("{Orderitems}")]
        [AbpMvcAuthorize]
        public IActionResult OrderItems(int ordid)
        {
            var model = orderService.GetOrderById(ordid);
            var obj = ObjectMapper.Map<OrdersViewModel>(model);
            return View(obj);
        }

        [HttpPut("{EditOrders}")]
        public IActionResult EditOrders([FromBody]OrdersViewModel ordersViewModel)
        {
            try
            {
                var order = objectMapper.Map<OrderBL>(ordersViewModel);
                orderService.ChangeOrder(order);
                return RedirectToAction("AddCart", "AddCart");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpDelete("{DeleteOrder}")]
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
