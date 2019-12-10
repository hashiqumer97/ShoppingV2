using Abp.Domain.Repositories;
using AutoMapper;
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
        private readonly IMapper mapper;
        private readonly IRepository<CustomerDL> customerRepository;

        public CustomerService(IMapper mapper, IRepository<CustomerDL> customerRepository)
        {
            this.mapper = mapper;
            this.customerRepository = customerRepository;
        }

        public void Create(CustomerBL customer)
        {
            try
            {
                var cust = mapper.Map<CustomerDL>(customer);
                customerRepository.Insert(cust);
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
                var query = customerRepository.GetAll();
                return mapper.Map<List<CustomerBL>>(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

