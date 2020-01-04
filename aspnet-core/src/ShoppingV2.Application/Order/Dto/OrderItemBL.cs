using Abp.Application.Services.Dto;
using ShoppingV2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingV2.BusinessObjects
{
    public class OrderItemBL: EntityDto<int>
    {
        public OrderItemBL()
        {

        }
        public string OrderitemDate { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ProductBL ProductName { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public OrderBL Orders { get; set; }
        public int OrderitemUnitPrice { get; set; }
        private int _OrderitemQuantity;
        public int OrderitemQuantity 
        {
            get
            {
                return _OrderitemQuantity;
            }
            set
            {
                if (_OrderitemQuantity >= 100)
                    throw new InvalidOperationException("Oh Sorry! Your quantity level had been exceeded!");

                _OrderitemQuantity = value;
            }

        }
        public int OrderitemProductPrice { get; set; }
        public bool IsDelete { get; set; }
        public int DiffQuantity { get; set; }

        public OrderItemBL(int orderitemid, bool isdelete)
        {
            Id = orderitemid;
            IsDelete = isdelete;
        }
        public OrderItemBL(int orderitemid, string orderitemdate, int productid, int orderid, int unitprice,
            int quantity, int productprice, bool isdelete)
        {
            Id = orderitemid;
            OrderitemDate = orderitemdate;
            ProductId = productid;
            OrderId = orderid;
            OrderitemUnitPrice = unitprice;
            OrderitemQuantity = quantity;
            OrderitemProductPrice = productprice;
            IsDelete = isdelete;

        }

        
    }
}
