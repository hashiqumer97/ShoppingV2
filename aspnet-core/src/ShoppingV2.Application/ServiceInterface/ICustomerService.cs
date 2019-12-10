using ShoppingV2.Application.BusinessObjects;
using ShoppingV2.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingV2.ServiceInterface
{
    public interface ICustomerService
    {
        List<CustomerBL> GetCustomers();
        void Create(CustomerBL customer);
    }
}
