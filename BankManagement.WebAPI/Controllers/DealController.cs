﻿using AutoMapper;
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
    public class DealController : Controller
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
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

        [Route("GetAllDeal")]
        [HttpGet]
        public IActionResult GetAllDeal (int? page)
        {
            try
            {
                var pageNumber = page ?? 1;
                int pageSize = 4;
                _logger.Trace("Access get all customer");
                var c = _dealService.GetAll();
                if (c == null)
                    _logger.Warn("List customer is null");
                var onePageOfDeals = c.ToPagedList(pageNumber, pageSize);
                _logger.Info("Get all customer successfully");
                return Ok(onePageOfDeals);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return BadRequest("Error: " + e.Message);
            }

        }

        [Route("FilterByDate")]
        [HttpGet]
        public IActionResult FilterByDate([FromForm]  string startDate, string endDate)
        {
            try
            {
                _logger.Trace("Access filter deal by date");
                var x = _dealService.SearchByDate(startDate, endDate);
                if (x == null)
                    _logger.Warn("Deal not found");
                _logger.Info("Filter deal from :"+startDate+""+"to"+endDate+"successfully");
                return Ok(x);
            }
            catch(Exception e)
            {
                _logger.Error(e.Message);
                return BadRequest(e.Message);
            }

        }

        [Route("GetDealByid/{id}")]
        [HttpGet]
        public IActionResult GetByid(int id)
        {
            try
            {
                _logger.Trace("Access search deal by id");
                var x = _dealService.GetByID(id);
                if (x == null)
                    _logger.Warn("Deal not found");
                _logger.Info("Get deal by id successfully");
                return Ok(x);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return BadRequest(e.Message);
            }
        }

        [Route("AddWithdraw")]
        [HttpPost]
        public IActionResult AddWithdraw([FromBody] int id, int currencyId, float withdraw )
        {
            try
            {
                _logger.Trace("Access add withdraw");
                var c =_dealService.Withdraw(id, currencyId, withdraw);
                if (c == null)
                    _logger.Warn("Param is invalid");
                _logger.Info("Deal withdraw :" +c.DealId);
                return Ok(c);
            }
            catch(Exception e)
            {
                _logger.Error(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
