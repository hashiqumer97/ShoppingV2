using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoppingV2.Entities
{
    public class CustomerDL:Entity<int>
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ContactNumber { get; set; }
    }
}
