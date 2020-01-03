using ShoppingV2.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingV2.ServiceInterface
{
    public interface IProductService
    {
        List<ProductBL> GetProducts();
        ProductBL GetProductSubCategories(int id);
        void Update(int productId, int quantity);
    }
}
