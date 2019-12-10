using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingV2.Web.Models.Customer;
using ShoppingV2.Web.Models.Order;
using ShoppingV2.Web.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingV2.Web.Models.AddCart
{
    public class AddCartViewModel
    {
        public List<SelectListItem> ProductListModel { get; set; }
        public int SelectedProductId { get; set; }
        public List<CustomerViewModel> SelectedCustomerName { get; set; }
        public List<ProductViewModel> SelectedProductName { get; set; }
        public List<OrderItemsViewModel> OrderItems { get; set; }


        public string ProductDescription { get; set; }

        [Display(Name = "")]
        public string CustomerName { get; set; }
        [Display(Name = "")]
        public string Date { get; set; }
        [Display(Name = "")]
        public string ProductName { get; set; }
        [Display(Name = "")]
        public int UnitPrice { get; set; }
        [Display(Name = "")]
        public int Quantity { get; set; }
        [Display(Name = "")]
        public int ProductPrice { get; set; }
    }
}
