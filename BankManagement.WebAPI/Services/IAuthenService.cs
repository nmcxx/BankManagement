using BankManagement.WebAPI.Entities;
using BankManagement.WebAPI.Helpers;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BankManagement.WebAPI.Services
{
    public interface IAuthenService
    {
        Customer Login(Customer model);

    }
    public class AuthenService : IAuthenService
    {

        public DataContext _db;
        public AuthenService(DataContext db)
        {
            _db = db;
        }
        public Customer Login(Customer model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return null;
            var obj = new Customer();
            try
            {
                obj = _db.Customers.SingleOrDefault(x => x.Email == model.Email);
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            if (obj == null)
                return null;
            if (!(obj.Password == (model.Password)))
                return null;
            return obj;
        }
    }
}
