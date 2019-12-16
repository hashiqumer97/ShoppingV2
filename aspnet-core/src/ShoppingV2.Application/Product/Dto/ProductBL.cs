using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoppingV2.BusinessObjects
{
    public class ProductBL: EntityDto<int>
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }

    }
}
