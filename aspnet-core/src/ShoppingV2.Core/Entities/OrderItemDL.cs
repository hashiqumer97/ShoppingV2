using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingV2.Entities
{
    public class OrderItemDL:Entity<int>
    {
        public string OrderitemDate { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual ProductDL ProductName { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual OrderDL Orders { get; set; }
        public int OrderitemUnitPrice { get; set; }
        public int OrderitemQuantity { get; set; }
        public int OrderitemProductPrice { get; set; }
    }
}
