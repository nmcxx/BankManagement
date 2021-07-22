using AutoMapper;
using BankManagement.WebAPI.Controllers;
using BankManagement.WebAPI.Entities;
using BankManagement.WebAPI.Models;
using BankManagement.WebAPI.Services;
using BankManagementTest.Services;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BankManagementTest
{
    public class CustomerTest
    {
        CustomerController customerController;
        ICustomerService customerService;
        private static IMapper _mapper;

        public CustomerTest()
        {

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            customerService = new CustomerServiceFake();
            customerController = new CustomerController(customerService,_mapper);
        }
        /// <summary>
        /// get all customer
        /// </summary>
        [Fact]
        public void GetAll_WhenCalled_ReturnOkResult()
        {
            // Act
            var okResult = customerController.Index();
            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = customerController.Index() as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<Customer>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        /// <summary>
        /// get customer by id
        /// </summary>
        [Fact]
        public void GetById_UnknownID_ReturnBadRequestResult()
        {
            // Act
            var notFoundResult = customerController.GetCustomerById(0);
            // Assert
            Assert.IsType<BadRequestObjectResult>(notFoundResult);
        }
        [Fact]
        public void GetById_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = customerController.GetCustomerById(1) as OkObjectResult;
            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }
        [Fact]
        public void GetById_ExistingID_ReturnsRightItem()
        {
            // Act
            var okResult = customerController.GetCustomerById(1) as OkObjectResult;
            // Assert
            Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(1, (okResult.Value as Customer).CustomerId);
        }
        /// <summary>
        /// Add new customer
        /// </summary>
        [Fact]
        public void Add_InvalidObjectPassed_ReturnBadRequestResult()
        {
            // Arrange
            var cusMissingItem = new CustomerModel()
            {
                CustomerName = "Guinness",
                Address = "Quy Nhon"
            };
            customerController.ModelState.AddModelError("Name", "Required");
            // Act
            var badResponse = customerController.AddCustomer(cusMissingItem);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        [Fact]
        public void Add_ValidObjectPassed_ReturnOkResult()
        {
            // Arrange
            CustomerModel testItem = new CustomerModel()
            {
                CustomerName = "Duy",
                Email = "testaa@gmail.com",
                Address = "Quy Nhon",
                PhoneNumber = "012345678",
                DateOfBirth = DateTime.Parse("01/01/1999")
            };
            // Act
            var createdResponse = customerController.AddCustomer(testItem) as OkObjectResult;
            var item = createdResponse.Value as Customer;
            // Assert
            Assert.IsType<Customer>(item);
            Assert.Equal("Duy", item.CustomerName);
        }
        /// <summary>
        /// Edit customer
        /// </summary>
        [Fact]
        public void Edit_UnknownID_ReturnBadRequestResult()
        {
            CustomerModel testItem = new CustomerModel()
            {
                CustomerName = "Duy",
                Email = "testaa@gmail.com",
                Address = "Quy Nhon",
                PhoneNumber = "012345678",
                DateOfBirth = DateTime.Parse("01/01/1999")
            };
            customerController.ModelState.AddModelError("Name", "Required");
            var badResponse = customerController.EditCustomer(testItem, 15);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        [Fact]
        public void Edit_ValidObjectPassed_ReturnOkResult()
        {
            // Arrange
            CustomerModel testItem = new CustomerModel()
            {
                CustomerName = "duy",
                Email = "duy@gmail.com",
                Address = "Quy Nhon",
                PhoneNumber = "0112345678",
                DateOfBirth = DateTime.Parse("01/01/1999"),
            };
            // Act
            var createdResponse = customerController.EditCustomer(testItem,1) as OkObjectResult;
            var item = createdResponse.Value as Customer;
            // Assert
            Assert.IsType<Customer>(item);
            Assert.Equal(1, item.CustomerId);
        }
        /// <summary>
        /// Delete Customer
        /// </summary>
        [Fact]
        public void Remove_NotExistingID_ReturnBadRequest()
        {
            // Act
            var badResponse = customerController.DeleteCustomer(0);
            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Act
            var okResponse = customerController.DeleteCustomer(1);
            var c = customerService.GetAll();
            // Assert
            Assert.IsType<OkObjectResult>(okResponse);
            Assert.Equal(2, c.Count());
        }
    }
}
