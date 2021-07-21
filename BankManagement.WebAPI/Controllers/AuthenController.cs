using AutoMapper;
using BankManagement.WebAPI.Entities;
using BankManagement.WebAPI.Models;
using BankManagement.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement.WebAPI.Controllers
{

    public class AuthenController : Controller
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private IMapper _mapper;
        private readonly IAuthenService _authenService;
        private readonly IConfiguration _configuration;
        public AuthenController(IAuthenService authenService,
           IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _authenService = authenService;
            _configuration = configuration;
        }

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                _logger.Trace("Access Login Controller");
                var user = _mapper.Map<Customer>(model);
                var a = _authenService.Login(user);

                if (a == null)
                {
                    _logger.Warn("User is not found");
                   return View();
                }
                _logger.Trace("Login successfully");
                var claim = new[] {
               new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Email)
                };
                var signinKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

                int expiryInMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

                var token = new JwtSecurityToken(
                  issuer: _configuration["Jwt:Site"],
                  audience: _configuration["Jwt:Site"],
                  expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                  signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );
                _logger.Info("token : " + token.ToString());
                return Ok(token);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return BadRequest("Error with exception: " + e);
            }
        }
        
    }
}
