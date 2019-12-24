using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingV2.Entities
{
    public class OrderDL:Entity<int>
    {
        public string ProductOrderDate { get; set; }
        public List<OrderItemDL> OrderLineItems { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]

        public CustomerDL Customers { get; set; }
    }
}
