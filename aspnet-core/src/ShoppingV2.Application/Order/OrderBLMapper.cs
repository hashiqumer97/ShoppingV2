using AutoMapper;
using ShoppingV2.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingV2.BusinessObjects.BusinessObjectsMapper
{
    public class OrderBLMapper:Profile
    {
        public OrderBLMapper()
        {
            CreateMap<OrderBL, OrderDL>().ReverseMap();
            CreateMap<OrderItemBL, OrderItemDL>().ReverseMap();
            CreateMap<OrderBL, OrderItemBL>().ReverseMap();
            CreateMap<OrderDL, OrderItemDL>().ReverseMap();
            CreateMap<OrderItemDL, OrderBL>().ReverseMap();
        }
    }
}
