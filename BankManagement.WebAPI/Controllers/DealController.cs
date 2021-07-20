using AutoMapper;
using BankManagement.WebAPI.Entities;
using BankManagement.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagement.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
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

        [Authorize]
        [Route("AddWithdraw")]
        [HttpPost]
        public IActionResult AddWithdraw([FromBody] int id, int currencyId, float withdraw )
        {
            try
            {
                var c =_dealService.Withdraw(id, currencyId, withdraw);
                return Ok(c);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
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
    }
}
