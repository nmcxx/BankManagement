using AutoMapper;
using BankManagement.WebAPI.Entities;
using BankManagement.WebAPI.Models;
using BankManagement.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
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

        #region Login  
        /// <summary>
        /// Login.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Login
        ///     {
        ///        "email": "string",
        ///        "password": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Status success and information customer</returns>
        /// <response code="201">Returns information customer</response>
        /// <response code="400">If the Customer is null or email or password not valid</response>
        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                var user = _mapper.Map<Customer>(model);
                var a = _authenService.Login(user);

                if (a == null)
                {
                   return BadRequest("Not found");
                }

                /*var claim = new[] {
               new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Email)
                };*/
                var signinKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

                int expiryInMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

                var token = new JwtSecurityToken(
                  issuer: _configuration["Jwt:Site"],
                  audience: _configuration["Jwt:Site"],
                  expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                  signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );
                string Token = new JwtSecurityTokenHandler().WriteToken(token);
                
                return Ok(Token);
            }
            catch (Exception e)
            {
                return BadRequest("Error with exception: " + e);
            }
        }
        #endregion
    }
}
