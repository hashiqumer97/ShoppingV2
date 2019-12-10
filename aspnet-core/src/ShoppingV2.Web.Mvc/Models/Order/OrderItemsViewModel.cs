using ShoppingV2.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingV2.Web.Models.Order
{
    public class OrderItemsViewModel
    {
        public int Id { get; set; }
        public string OrderitemDate { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int OrderitemUnitPrice { get; set; }
        public int OrderitemQuantity { get; set; }
        public int OrderitemProductPrice { get; set; }
        public bool IsDelete { get; set; }
    }
}
