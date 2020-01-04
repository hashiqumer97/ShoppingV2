using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using ShoppingV2.Application.BusinessObjects;
using ShoppingV2.BusinessObjects;
using ShoppingV2.Entities;
using ShoppingV2.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingV2.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IObjectMapper mapper;
        private readonly IRepository<CustomerDL> customerRepository;

        public CustomerService(IObjectMapper mapper, IRepository<CustomerDL> customerRepository)
        {
            this.mapper = mapper;
            this.customerRepository = customerRepository;
        }
        public void Create(CustomerBL customer)
        {
            try
            {
                var createCustomer = mapper.Map<CustomerDL>(customer);
                customerRepository.Insert(createCustomer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CustomerBL> GetCustomers()
        {
            try
            {
                var getCustomers = customerRepository.GetAll();
                return mapper.Map<List<CustomerBL>>(getCustomers);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

