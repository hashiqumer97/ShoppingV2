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
        public int ProductId { get; set; }
        public string ProductOrderDate { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductPrice { get; set; }
        public int CustomerId { get; set; }

        public CustomerBL Customers { get; set; }
        public List<OrderItemBL> OrderLineItems { get; set; }

        public bool IsDelete { get; set; }

    }
}
