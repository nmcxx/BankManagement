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
    public class DealController : Controller
    {
        private IMapper _mapper;
        private readonly IDealService _dealService;
        private readonly IConfiguration _configuration;
        public DealController(IDealService dealService,
           IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _dealService = dealService;
            _configuration = configuration;
        }

        #region AddWithdraw
        /// <summary>
        /// Withdraw money.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /AddWithdraw
        ///     {
        ///          "currencyid": "int",
        ///          "withdraw": "float",
        ///          "customerid": "int",
        ///      }
        ///
        /// </remarks>
        /// <param name="customerid" name="currencyId" name="withdraw"></param>
        /// <returns>Status success</returns>
        /// <response code="201">Returns information customer</response>
        /// <response code="400">Value is not a valid </response>
        [Route("AddWithdraw")]
        [HttpPost]
        public IActionResult AddWithdraw([FromForm] int id, int currencyId, float withdraw)
        {
            try
            {
                var c = _dealService.Withdraw(id, currencyId, withdraw);
                return Ok(c);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region Transfer
        /// <summary>
        /// Transfer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Transfer
        ///     {
        ///          "DealId": "int",
        ///          "Money": "number",
        ///          "Date": "datetime",
        ///          "CustomerIdSend": "int",
        ///          "CustomerIdRevice": "int",
        ///          "CustomerId": "CustomerId",
        ///          "CustomerName": "CustomerName",
        ///      }
        ///
        /// </remarks>
        /// <param name="deal"></param>
        /// <returns>Status success</returns>
        /// <response code="201">Returns information customer</response>
        /// <response code="400">Value is not a valid </response>
        [Route("Transfer")]
        [HttpPost]
        public IActionResult Transfer(Deal deal)
        {
            try
            {
                var c = _dealService.Add(deal);
                return Ok(c);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region GetById
        /// <summary>
        /// Get deal by Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetById
        ///     {
        ///          
        ///      }
        ///
        /// </remarks>
        /// <param name="id" ></param>
        /// <returns>Status success</returns>
        /// <response code="201">Returns information customer</response>
        /// <response code="400">Value is not a valid </response>
        [Route("GetById/{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var deal = _dealService.GetByID(id);
            //var model = _mapper.Map<DealModel>(deal);
            return Ok(deal);
        }
        #endregion

        #region GetByIdCus
        /// <summary>
        /// Get deal by customer id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /GetByIdCus
        ///     {
        ///          
        ///      }
        ///
        /// </remarks>
        /// <param name="id" ></param>
        /// <returns>Status success</returns>
        /// <response code="201">Returns information customer</response>
        /// <response code="400">Value is not a valid </response>
        [Route("GetByIdCus/{id}")]
        [HttpGet]
        public IActionResult GetByIdCus(int id)
        {
            var deal = _dealService.GetByIDCus(id);
            //var model = _mapper.Map<DealModel>(deal);
            return Ok(deal);
        }
        #endregion
    }
}
