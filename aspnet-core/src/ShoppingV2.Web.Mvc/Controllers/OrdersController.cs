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
            var getOrders = objectMapper.Map<IEnumerable<OrdersViewModel>>(orderService.GetOrders());
            return View(getOrders);
        }
        [HttpGet]
        [AbpMvcAuthorize]
        public IActionResult OrderItems(int ordid)
        {
            var getOrdersById = orderService.GetOrderById(ordid);
            var viewOrdersById = objectMapper.Map<OrdersViewModel>(getOrdersById);
            return View(viewOrdersById);
        }
    }
}
