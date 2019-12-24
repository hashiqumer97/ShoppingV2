using Abp.Application.Services.Dto;
using ShoppingV2.Application.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ShoppingV2.BusinessObjects
{
    public class OrderBL : EntityDto<int>
    {
        public OrderBL()
        {

        }
        public string ProductOrderDate { get; set; }
        public int CustomerId { get; set; }
        public CustomerBL Customers { get; set; }
        public List<OrderItemBL> OrderLineItems { get; set; }
        public bool IsDelete { get; set; }

        public OrderBL(int customerId, List<OrderItemBL> orderItems,
            string productOrderDate)
        {
            CustomerId = customerId;
            OrderLineItems = orderItems;
            ProductOrderDate = productOrderDate;
        }
        public OrderBL(int orderId)
        {
            Id = orderId;
        }
    }
}
