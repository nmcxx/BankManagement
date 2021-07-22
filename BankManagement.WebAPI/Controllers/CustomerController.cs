using AutoMapper;
using BankManagement.WebAPI.Entities;
using BankManagement.WebAPI.Models;
using BankManagement.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace BankManagement.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
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

        #region GetAllCustomer
        /// <summary>
        /// Get all customer.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns>Status success and information customer</returns>
        /// <response code="201">Returns information customer</response>
        /// <response code="400">If the list customer is null</response>
        [Route("GetAllCustomer")]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                
                _logger.Trace("Access get all customer");
                var c = _customerService.GetAll();
                if (c == null)
                    throw new Exception("List customer is null");
                
                _logger.Info("Get all customer successfully");
                return Ok(c);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return BadRequest("Error: " + e.Message);
            }

        }
        [Route("GetCustomerById")]
        [HttpGet]
        public IActionResult GetCustomerById(int id)
        {
            try
            {
                _logger.Trace("Access get customer by id");
                var c = _customerService.GetById(id);
                if (c == null)
                    throw new Exception("Get failed");
                _logger.Info("Get customer" + c.CustomerId + "" + " successfully");
                return Ok(c);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return BadRequest("Error: " + e.Message);
            }
        }
        #endregion

        #region AddCustomer
        /// <summary>
        /// Add new Customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /AddCustomer
        ///     {
        ///         "customerName": "string",
        ///         "email": "string",
        ///         "address": "string",
        ///         "phoneNumber": "string",
        ///         "accountNumber": "string",
        ///         "dateOfBirth": "yyyy-dd-mm"
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Status success and information customer</returns>
        /// <response code="201">Returns information customer</response>
        /// <response code="400">Value is not a valid </response>
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
        #endregion

        #region EditCustomer
        /// <summary>
        /// Edit Customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /EditCustomer
        ///     {
        ///          "customerName": "string",
        ///          "email": "string",
        ///          "address": "string",
        ///          "phoneNumber": "string",
        ///          "accountNumber": "string",
        ///          "dateOfBirth": "yyyy-mm-dd"
        ///      }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Status success and information customer</returns>
        /// <response code="201">Returns information customer</response>
        /// <response code="400">Value is not a valid </response>
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
        #endregion

        #region DeleteCustomer
        /// <summary>
        /// Delete Customer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /DeleteCustomer
        ///     {
        ///     
        ///      }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Status success and information customer</returns>
        /// <response code="201">Returns information customer</response>
        /// <response code="400">Value is not a valid </response>
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
        #endregion

        #region GetByIdCustomer
        /// <summary>
        /// Get customer by IdCustomer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetByIdCustomer
        ///     {
        ///     
        ///      }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Status success and information customer</returns>
        /// <response code="201">Returns information customer</response>
        /// <response code="400">Value is not a valid </response>
        [Route("GetByIdCustomer/{id}")]
        [HttpGet]
        public IActionResult GetByIdCustomer(int id)
        {
            var user = _customerService.GetById(id);
            var model = _mapper.Map<CustomerModel>(user);
            return Ok(model);
        }
        #endregion
    }
}
