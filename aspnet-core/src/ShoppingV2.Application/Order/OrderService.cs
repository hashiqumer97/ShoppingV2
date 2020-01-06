using Abp.Domain.Repositories;
using Abp.Domain.Uow;
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
        private readonly IObjectMapper objectMapper;
        private readonly IProductService productService;
        private readonly IRepository<OrderDL> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<ProductDL> productRepository;

        public OrderService(Abp.ObjectMapping.IObjectMapper objectMapper, IProductService productService,
            IRepository<OrderDL> repository, IUnitOfWork unitOfWork,
            IRepository<ProductDL> productRepository)
        {
            this.objectMapper = objectMapper;
            this.productService = productService;
            this.repository = repository;
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
                if (item.OrderitemQuantity >= 100)
                    throw new InvalidOperationException("Quantity has been exceeded!");
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
            unitOfWork.SaveChanges();
        }
        public void ChangeOrder(OrderBL items)
        {
            var tempOrder = repository.GetAllIncluding().Include(i => i.OrderLineItems).First(o => o.Id == items.Id);
            foreach (var item in items.OrderLineItems)
            {
                var getProductId = productRepository.Get(item.ProductId);
                if (!item.IsDelete)
                    if (item.OrderitemQuantity >= 100)
                        throw new InvalidOperationException("Quantity has been exceeded!");
                    if (getProductId.Quantity <= 0)
                        throw new InvalidOperationException("Quantity is over!");
                var tempOrdLine = tempOrder.OrderLineItems.FirstOrDefault(f => f.Id == item.Id);
                var tempDiff = tempOrdLine.OrderitemQuantity - item.OrderitemQuantity;
                tempOrdLine.ProductId = item.ProductId;
                tempOrdLine.OrderitemQuantity = item.OrderitemQuantity;
                tempOrdLine.OrderitemDate = item.OrderitemDate;
                tempOrdLine.OrderitemProductPrice = item.OrderitemProductPrice;
                productService.UpdateProductQuantity(item.ProductId, tempDiff);
                repository.Update(tempOrder);
                if (item.IsDelete)
                    RemoveOrder(items);
            }
            unitOfWork.SaveChanges();
        }
        private void RemoveOrder(OrderBL items)
        {
            var tempOrder = repository.GetAllIncluding().Include(i => i.OrderLineItems).First(o => o.Id == items.Id);
            foreach (var item in items.OrderLineItems)
            {
                var ordItem = tempOrder.OrderLineItems.FirstOrDefault(o => o.Id == item.Id);
                var deleteDiff = ordItem.OrderitemQuantity + 0;
                productService.UpdateProductQuantity(item.ProductId, deleteDiff);
                tempOrder.OrderLineItems.Remove(ordItem);
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

