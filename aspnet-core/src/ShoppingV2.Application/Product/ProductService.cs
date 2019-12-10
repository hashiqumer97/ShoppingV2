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

        public ProductBL GetProductSubCategories(int id)
        {
            try
            {
                var query = productRepository.Get(id);
                return mapper.Map<ProductBL>(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(int productId, int quantity)
        {
            var productBO = productRepository.Get(productId);

            productBO.Quantity = productBO.Quantity + quantity;

            var product = mapper.Map<ProductDL>(productBO);
            productRepository.Update(product);
        }
    }
}
