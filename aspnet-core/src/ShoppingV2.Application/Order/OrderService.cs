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
            return objectMapper.Map<List<OrderBL>>(repository.GetAll());
        }
        public void CreateOrder(OrderBL order)
        {
           
            foreach (var item in order.OrderLineItems)
            {
                var productBO = productRepository.Get(item.ProductId);
                if (productBO.Quantity <= 0)
                {
                    throw new InvalidOperationException("Quantity is over!");
                }
                else
                {
                    var ordtemp = order.Create(order.CustomerId, order.OrderLineItems, order.ProductOrderDate);
                    var ord = objectMapper.Map<OrderDL>(ordtemp);
                    repository.Insert(ord);
                    productService.Update(item.ProductId, -(item.OrderitemQuantity));
                }

            }
            unitOfWork.SaveChanges();
        }
        public void DeleteEntireOrder(OrderBL orders)
        {
            var orddelete = orders.Delete(orders.Id);
            var order = objectMapper.Map<OrderDL>(orddelete);
            repository.Delete(order);
        }
        public void ChangeOrder(OrderBL items)
        {

            foreach (var item in items.OrderLineItems)
            {
                var tempordline = itemrepository.Get(item.Id);
                var prodqty = productRepository.Get(item.ProductId);
                
                if (item.IsDelete)
                {
                    itemrepository.Delete(tempordline);
                }
                else
                {
                    if(prodqty.Quantity <= 0)
                    {
                        throw new InvalidOperationException("Quantity is over!");
                    }
                    else
                    {
                        item.Update(item.Id, item.OrderitemDate, item.ProductId, item.OrderId,
                    item.OrderitemUnitPrice, item.OrderitemQuantity, item.OrderitemProductPrice
                    , tempordline.OrderitemQuantity, item.IsDelete);
                        tempordline.OrderitemQuantity = item.OrderitemQuantity;
                        tempordline.OrderitemDate = item.OrderitemDate;
                        tempordline.OrderitemProductPrice = item.OrderitemProductPrice;
                        itemrepository.Update(tempordline);
                        productService.Update(item.ProductId, item.DiffQuantity);
                    }
                }
            }
            unitOfWork.SaveChanges();
        }
        public OrderBL GetOrderById(int id)
        {
            var getord = repository.GetAllIncluding().Include(i => i.OrderLineItems).First(o => o.Id == id);
            return objectMapper.Map<OrderBL>(getord);
        }
    }
}

