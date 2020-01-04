using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingV2.Controllers;
using ShoppingV2.ServiceInterface;
using ShoppingV2.Web.Models.Customer;

namespace ShoppingV2.Web.Controllers
{
    public class CustomerController : ShoppingV2ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var getCustomers = _customerService.GetCustomers();
            var viewCustomers = ObjectMapper.Map<IEnumerable<CustomerViewModel>>(getCustomers);
            return View(viewCustomers);
        }
    }
}