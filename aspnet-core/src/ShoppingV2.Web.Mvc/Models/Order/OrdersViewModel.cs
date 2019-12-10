using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingV2.Web.Models.Order
{
    public class OrdersViewModel
    {
        public int Id { get; set; }
        public string ProductOrderDate { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItemsViewModel> OrderLineItems { get; set; }
    }
}
