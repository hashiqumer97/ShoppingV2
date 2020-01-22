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

            foreach (var item in orderItems)
            {
                if (item.OrderitemQuantity >= 100)
                    throw new InvalidOperationException("Quantity has been exceeded!");
                if (item.OrderitemQuantity == 0)
                    throw new InvalidOperationException("Please enter the quantity!");
            }
        }
        public OrderBL(List<OrderItemBL> orderItems)
        {
            OrderLineItems = orderItems;
            foreach (var item in orderItems)
            {
                if (item.OrderitemQuantity >= 100)
                    throw new InvalidOperationException("Quantity has been exceeded!");
                if (item.OrderitemQuantity == 0)
                    throw new InvalidOperationException("Please enter the quantity!");
            }
        }
        public OrderBL(int orderId)
        {
            Id = orderId;
        }
    }
}
