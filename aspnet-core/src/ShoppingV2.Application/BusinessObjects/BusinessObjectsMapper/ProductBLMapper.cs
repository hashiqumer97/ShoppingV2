using AutoMapper;
using ShoppingV2.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingV2.BusinessObjects.BusinessObjectsMapper
{
    public class ProductBLMapper:Profile
    {
        public ProductBLMapper()
        {
            CreateMap<ProductBL, ProductDL>().ReverseMap();
        }
    }
}
