using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingV2.BusinessObjects;
using ShoppingV2.Entities;
using ShoppingV2.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.ObjectMapping;

namespace ShoppingV2.Service
{
    public class OrderService : IOrderService
    {
        private readonly Abp.ObjectMapping.IObjectMapper objectMapper;
        private readonly IProductService productService;
        private readonly IRepository<OrderDL> repository;
        private readonly IRepository<OrderItemDL> itemrepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<ProductDL> productRepository;

        public OrderService(Abp.ObjectMapping.IObjectMapper objectMapper, IProductService productService,
            IRepository<OrderDL> repository, IRepository<OrderItemDL> itemrepository, IUnitOfWork unitOfWork,
            IRepository<ProductDL> productRepository)
        {
            this.objectMapper = objectMapper;
            this.productService = productService;
            this.repository = repository;
            this.itemrepository = itemrepository;
            this.unitOfWork = unitOfWork;
            this.productRepository = productRepository;
        }
        public List<OrderBL> GetOrders()
        {
            var getOrder = repository.GetAll().ToList();
            return objectMapper.Map<List<OrderBL>>(getOrder);
        }
        public void CreateOrder(OrderBL order)
        {
            foreach (var item in order.OrderLineItems)
            {
                var getProductId = productRepository.Get(item.ProductId);
                if (getProductId.Quantity <= 0)
                    throw new InvalidOperationException("Quantity is over!");
                productService.UpdateProductQuantity(item.ProductId, -(item.OrderitemQuantity));
            }
            var newOrder = new OrderBL(order.CustomerId, order.OrderLineItems, order.ProductOrderDate);
            repository.Insert(objectMapper.Map<OrderDL>(newOrder));
            unitOfWork.SaveChanges();
        }
        public void DeleteEntireOrder(OrderBL orders)
        {
            var getOrderId = new OrderBL(orders.Id);
            var deleteOrder = objectMapper.Map<OrderDL>(getOrderId);
            repository.Delete(deleteOrder);
        }
        public void ChangeOrder(OrderBL items)
        {
            
            foreach (var item in items.OrderLineItems)
            {
                var temp = itemrepository.Get(item.Id);
                var orderLines = new OrderItemBL(item.Id, item.OrderitemDate, item.ProductId, item.OrderId, item.OrderitemUnitPrice
                    , item.OrderitemQuantity, item.OrderitemProductPrice, item.IsDelete);
                var getProductQuantity = productRepository.Get(item.ProductId);
                var tempDiff = temp.OrderitemQuantity - item.OrderitemQuantity;
                if (item.IsDelete)
                    itemrepository.Delete(objectMapper.Map<OrderItemDL>(orderLines));
                if (getProductQuantity.Quantity <= 0)
                    throw new InvalidOperationException("Quantity is over!");
                if (!item.IsDelete)
                    orderLines.OrderitemQuantity = item.OrderitemQuantity;
                    orderLines.OrderitemDate = item.OrderitemDate;
                    orderLines.OrderitemProductPrice = item.OrderitemProductPrice;
                    itemrepository.Update(objectMapper.Map<OrderItemDL>(orderLines));
                productService.UpdateProductQuantity(item.ProductId, tempDiff);
            }
            unitOfWork.SaveChanges();
        }
        public OrderBL GetOrderById(int id)
        {
            var getOrder = repository.GetAllIncluding().Include(i => i.OrderLineItems).Include(c => c.Customers).FirstOrDefault(o => o.Id == id);
            return objectMapper.Map<OrderBL>(getOrder);
        }
    }
}

