using ShoppingV2.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingV2.ServiceInterface
{
    public interface IOrderService
    {
        List<OrderBL> GetOrders();
        OrderBL GetOrderById(int id);
        void CreateOrder(OrderBL order);
        void DeleteEntireOrder(OrderBL orders);
        void ChangeOrder(OrderBL orderBO);
    }
}
