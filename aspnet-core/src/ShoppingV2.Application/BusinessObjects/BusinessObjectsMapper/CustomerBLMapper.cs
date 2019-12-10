using AutoMapper;
using ShoppingV2.Application.BusinessObjects;
using ShoppingV2.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingV2.BusinessObjects.BusinessObjectsMapper
{
    public class CustomerBLMapper:Profile
    {
        public CustomerBLMapper()
        {
            CreateMap<CustomerBL, CustomerDL>().ReverseMap();
        }
    }
}
