using Abp.Domain.Repositories;
using AutoMapper;
using ShoppingV2.BusinessObjects;
using ShoppingV2.Entities;
using ShoppingV2.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingV2.Service
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly IRepository<ProductDL> productRepository;

        public ProductService(IRepository<OrderDL> repository, IMapper mapper, IRepository<ProductDL> productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }
        public List<ProductBL> GetProducts()
        {
            return mapper.Map<List<ProductBL>>(productRepository.GetAll());
        }
        public ProductBL GetProductById(int id)
        {
            try
            {
                var getProductId = productRepository.Get(id);
                return mapper.Map<ProductBL>(getProductId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateProductQuantity(int productId, int quantity)
        {
            var getProductId = productRepository.Get(productId);
            getProductId.Quantity = getProductId.Quantity + quantity;
            var updateProductQuantity = mapper.Map<ProductDL>(getProductId);
            productRepository.Update(updateProductQuantity);
        }
    }
}
