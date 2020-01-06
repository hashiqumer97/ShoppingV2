using System;
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

namespace ShoppingV2.Web.Host.Controllers.v1
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
        [HttpGet("ViewOrders")]
        [AbpMvcAuthorize]
        public IActionResult ViewOrders()
        {
            var getOrders = objectMapper.Map<List<OrdersViewModel>>(orderService.GetOrders());
            return View(getOrders);
        }
        [HttpGet("ViewOrderItems")]
        [AbpMvcAuthorize]
        public IActionResult ViewOrderItems(int ordid)
        {
            var getOrderById = orderService.GetOrderById(ordid);
            objectMapper.Map<OrdersViewModel>(getOrderById);
            return Ok();
        }
        [HttpPut("UpdateOrders")]
        public IActionResult UpdateOrders([FromBody]OrdersViewModel ordersViewModel)
        {
            try
            {
                var updateOrders = objectMapper.Map<OrderBL>(ordersViewModel);
                orderService.ChangeOrder(updateOrders);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("DeleteOrders")]
        public IActionResult DeleteOrders([FromBody]OrdersViewModel ordersViewModel)
        {
            var deleteOrders = objectMapper.Map<OrderBL>(ordersViewModel);
            orderService.ChangeOrder(deleteOrders);
            return Ok();
        }
        [HttpDelete("DeleteEntireOrder")]
        public IActionResult DeleteEntireOrder([FromBody]OrdersViewModel ordersViewModel)
        {
            var deleteEntireOrder = objectMapper.Map<OrderBL>(ordersViewModel);
            orderService.DeleteEntireOrder(deleteEntireOrder);
            return Ok();
        }
    }
}
