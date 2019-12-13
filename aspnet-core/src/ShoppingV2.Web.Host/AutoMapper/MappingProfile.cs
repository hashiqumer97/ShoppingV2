using AutoMapper;
using ShoppingV2.Application.BusinessObjects;
using ShoppingV2.BusinessObjects;
using ShoppingV2.Web.Models.Customer;
using ShoppingV2.Web.Models.Order;
using ShoppingV2.Web.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingV2.Web.Host.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductViewModel, ProductBL>().ReverseMap();
            CreateMap<OrdersViewModel, OrderBL>().ReverseMap();
            CreateMap<OrderItemsViewModel, OrderItemBL>().ReverseMap();
            CreateMap<OrdersViewModel, OrderItemsViewModel>().ReverseMap();
            CreateMap<CustomerViewModel, CustomerBL>().ReverseMap();
        }
    }
}
