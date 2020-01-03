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
            var orders = repository.GetAll().ToList();
            return objectMapper.Map<List<OrderBL>>(orders);
        }
        public void CreateOrder(OrderBL order)
        {
            foreach (var item in order.OrderLineItems)
            {
                var newOrder = new OrderBL(order.CustomerId, order.OrderLineItems, order.ProductOrderDate);
                var productBO = productRepository.Get(item.ProductId);
                if (productBO.Quantity <= 0)
                    throw new InvalidOperationException("Quantity is over!");
                var ord = objectMapper.Map<OrderDL>(newOrder);
                repository.Insert(ord);
            }
            unitOfWork.SaveChanges();
        }
        public void DeleteEntireOrder(OrderBL orders)
        {
            var orddelete = new OrderBL(orders.Id);
            var order = objectMapper.Map<OrderDL>(orddelete);
            repository.Delete(order);
        }
        public void ChangeOrder(OrderBL items)
        {
            foreach (var item in items.OrderLineItems)
            {
                var tempordline = new OrderItemBL(item.Id, item.OrderitemDate, item.ProductId, item.OrderId, item.OrderitemUnitPrice,
                        item.OrderitemQuantity, item.OrderitemProductPrice);
                var deletetempordline = new OrderItemBL(item.Id);
                var prodqty = productRepository.Get(item.ProductId);
                if (item.IsDelete == true)
                    itemrepository.Delete(objectMapper.Map<OrderItemDL>(deletetempordline));
                if (prodqty.Quantity <= 0)
                    throw new InvalidOperationException("Quantity is over!");
                if (item.IsDelete == false)
                    tempordline.OrderitemDate = item.OrderitemDate;
                    tempordline.OrderitemQuantity = item.OrderitemQuantity;
                    tempordline.OrderitemProductPrice = item.OrderitemProductPrice;
                    itemrepository.Update(objectMapper.Map<OrderItemDL>(tempordline));
                    productService.Update(item.ProductId, item.DiffQuantity);
            }
        unitOfWork.SaveChanges();
        }
    public OrderBL GetOrderById(int id)
    {
        var getord = repository.GetAllIncluding().Include(i => i.OrderLineItems).Include(c => c.Customers).First(o => o.Id == id);
        return objectMapper.Map<OrderBL>(getord);
    }
}
}

