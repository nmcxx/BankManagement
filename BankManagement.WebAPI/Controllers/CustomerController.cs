using AutoMapper;
using BankManagement.WebAPI.Entities;
using BankManagement.WebAPI.Models;
using BankManagement.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagement.WebAPI.Controllers
{
    public class CustomerController : Controller
    {
        private IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;
        public CustomerController(ICustomerService customerService,
           IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _customerService = customerService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("AddCustomer")]
        [HttpPost]
        public IActionResult AddCustomer([FromBody]CustomerModel model)
        {
            try
            {
                var customer = _mapper.Map<Customer>(model);
                var c = _customerService.Add(customer);
                return Ok(c);
            }
            catch(Exception e)
            {
                return BadRequest("Error: " + e.Message);
            }
        }
    }
}
