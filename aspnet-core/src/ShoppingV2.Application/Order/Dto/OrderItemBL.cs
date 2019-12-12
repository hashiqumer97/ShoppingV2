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
        public string OrderitemDate { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ProductBL ProductName { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public OrderBL Orders { get; set; }
        public int OrderitemUnitPrice { get; set; }
        public int OrderitemQuantity { get; set; }
        public int OrderitemProductPrice { get; set; }
        public bool IsDelete { get; set; }
        public int DiffQuantity { get; set; }

        public OrderItemBL Update(int orditemid, string orderItemDate, int productId, int orderId,
        int unitPrice, int quantity, int productPrice, int updatedQuantity, bool isdelete)
        {
            Id = orditemid;
            OrderitemDate = orderItemDate;
            ProductId = productId;
            OrderId = orderId;
            OrderitemUnitPrice = unitPrice;
            OrderitemQuantity = quantity;
            OrderitemProductPrice = productPrice;
            DiffQuantity = (updatedQuantity - quantity);
            IsDelete = isdelete;

            if(quantity >= 100)
            {
                throw new InvalidOperationException("Oh Sorry! Your Order cannot be updated because the quantity is over the limit!");
            }
            return this;
        }
    }

    

}
