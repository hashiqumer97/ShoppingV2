using Abp.Application.Services.Dto;
using ShoppingV2.Application.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoppingV2.BusinessObjects
{
    public class OrderBL:EntityDto<int>
    {
        public string ProductOrderDate { get; set; }
        public int CustomerId { get; set; }

        public CustomerBL Customers { get; set; }
        public List<OrderItemBL> OrderLineItems { get; set; }

        public bool IsDelete { get; set; }

        public OrderBL Create(int customerId, List<OrderItemBL> orderItems,
            string productOrderDate)
        {
            CustomerId = customerId;
            OrderLineItems = orderItems;
            ProductOrderDate = productOrderDate;


            foreach(var item in orderItems)
            {
                if(item.OrderitemQuantity >= 100)
                {
                    throw new InvalidOperationException("Oh Sorry! Your Order cannot be added because the quantity is over the limit!");
                }

                if(item.OrderitemQuantity == 0 || item.OrderitemQuantity.ToString() == null)
                {
                    throw new InvalidOperationException("Oh Sorry! Your order cannot be created because the quantity is not included!");
                }
               
            }
            return this;
        }

        public OrderBL Delete(int orderId)
        {
            Id = orderId;
            return this;
        }

    }
}
