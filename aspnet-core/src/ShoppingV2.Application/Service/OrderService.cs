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

        public OrderService(Abp.ObjectMapping.IObjectMapper objectMapper, IProductService productService,
            IRepository<OrderDL> repository, IRepository<OrderItemDL> itemrepository, IUnitOfWork unitOfWork)
        {
            this.objectMapper = objectMapper;
            this.productService = productService;
            this.repository = repository;
            this.itemrepository = itemrepository;
            this.unitOfWork = unitOfWork;
        }
        public List<OrderBL> GetOrders()
        {
            return objectMapper.Map<List<OrderBL>>(repository.GetAll());
        }
        public void CreateOrder(OrderBL order)
        {
            var ord = objectMapper.Map<OrderDL>(order);
            repository.Insert(ord);
            foreach (var item in order.OrderLineItems)
            {
                productService.Update(item.ProductId, -(item.OrderitemQuantity));
            }
            unitOfWork.SaveChanges();
        }
        public void DeleteEntireOrder(OrderBL orders)
        {
            var order = objectMapper.Map<OrderDL>(orders);
            repository.Delete(order);
        }
        public void ChangeOrder(OrderBL items)
        {
            foreach (var item in items.OrderLineItems)
            {
                var tempordline = itemrepository.Get(item.Id);
                var tempdiff = tempordline.OrderitemQuantity - item.OrderitemQuantity;
                tempordline.OrderitemQuantity = item.OrderitemQuantity;
                tempordline.OrderitemProductPrice = item.OrderitemProductPrice;
                tempordline.OrderitemDate = item.OrderitemDate;
                if (item.IsDelete)
                {
                    var orderitem = objectMapper.Map<OrderItemDL>(item);
                    if (orderitem is null) continue;
                    itemrepository.Delete(tempordline);
                }
                else
                {
                    var orderitem = objectMapper.Map<OrderItemDL>(item);
                    if (orderitem is null) continue;
                    itemrepository.Update(tempordline);
                }
                productService.Update(item.ProductId, tempdiff);
            }
        }
        public OrderBL GetOrderById(int id)
        {
            var getord = repository.GetAllIncluding().Include(i => i.OrderLineItems).First(o => o.Id == id);
            return objectMapper.Map<OrderBL>(getord);
        }
    }
}

