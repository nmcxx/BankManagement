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
using X.PagedList;

namespace BankManagement.WebAPI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private IMapper _mapper;
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService,
           IMapper mapper)
        {
            _mapper = mapper;
            _customerService = customerService;
        }

        [Route("GetAllCustomer")]
        [HttpGet]
        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page ?? 1;
                int pageSize = 4;
                _logger.Trace("Access get all customer");
                var c = _customerService.GetAll();
                if (c == null)
                    throw new Exception("List customer is null");
                var onePageOfCustomers = c.ToPagedList(pageNumber, pageSize);
                _logger.Info("Get all customer successfully");
                return Ok(onePageOfCustomers);
            }
            catch(Exception e)
            {
                _logger.Error(e.Message);
                return BadRequest("Error: " + e.Message);
            }
            
        }

        [Route("AddCustomer")]
        [HttpPost]
        public IActionResult AddCustomer([FromBody] CustomerModel model)
        {
            try
            {
                _logger.Trace("Access add new customer");
                var customer = _mapper.Map<Customer>(model);
                var c = _customerService.Add(customer);
                if (c == null)
                    throw new Exception("Add failed");
                _logger.Info("Add new customer"+ c.CustomerId+""+" successfully");
                return Ok(c);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return BadRequest("Error: " + e.Message);
            }
        }

        [Route("EditCustomer/{id}")]
        [HttpPut]
        public IActionResult EditCustomer([FromBody] CustomerModel model, int id)
        {
            try
            {
                _logger.Trace("Access edit customer");
                var customer = _mapper.Map<Customer>(model);
                customer.CustomerId = id;
                var c = _customerService.Edit(customer);
                if (c == null)
                    throw new Exception("Customer is not found");
                _logger.Info("Edit customer" + c.CustomerId+""+"successfully");
                return Ok(c);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return BadRequest("Error: " + e.Message);
            }
        }

        [Route("DeleteCustomer/{id}")]
        [HttpDelete]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                _logger.Trace("Access delete customer");
                _customerService.Delete(id);
                _logger.Info("Delete customer " + id + "" + "successfully");
                return Ok("Delete successfully !");
            }
            catch(Exception e)
            {
                _logger.Error(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
