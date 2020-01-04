using ShoppingV2.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingV2.ServiceInterface
{
    public interface IProductService
    {
        List<ProductBL> GetProducts();
        ProductBL GetProductById(int id);
        void UpdateProductQuantity(int productId, int quantity);
    }
}
