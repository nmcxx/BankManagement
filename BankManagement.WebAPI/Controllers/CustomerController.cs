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

        [Route("GetAllCustomer")]
        [HttpGet]
        public IActionResult Index()
        {
            var c = _customerService.GetAll();
            return Ok(c);
        }

        [Route("AddCustomer")]
        [HttpPost]
        public IActionResult AddCustomer([FromBody] CustomerModel model)
        {
            try
            {
                var customer = _mapper.Map<Customer>(model);
                var c = _customerService.Add(customer);
                return Ok(c);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.Message);
            }
        }

        [Route("EditCustomer/{id}")]
        [HttpPut]
        public IActionResult EditCustomer([FromBody] CustomerModel model, int id)
        {
            try
            {
                var customer = _mapper.Map<Customer>(model);
                customer.CustomerId = id;
                var c = _customerService.Edit(customer);
                return Ok(c);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.Message);
            }
        }

        [Route("DeleteCustomer/{id}")]
        [HttpDelete]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                _customerService.Delete(id);
                return Ok("Delete successfully !");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("GetByIdCustomer/{id}")]
        [HttpGet]
        public IActionResult GetByIdCustomer(int id)
        {
            var user = _customerService.GetById(id);
            var model = _mapper.Map<CustomerModel>(user);
            return Ok(model);
        }
    }
}
