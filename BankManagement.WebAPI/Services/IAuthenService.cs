using BankManagement.WebAPI.Entities;
using BankManagement.WebAPI.Helpers;
using ProductManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Globalization;

namespace BankManagement.WebAPI.Services
{
    public interface IAuthenService
    {
        Customer Login(Customer model);
        Customer Register(Customer _user);
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
            if (!(obj.Password == GetMD5(model.Password)))
                return null;
            return obj;
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public Customer Register(Customer _user)
        {
            if (string.IsNullOrWhiteSpace(_user.Password))
                throw new AppException("Password is required");
            string email = _user.Email;
            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    string domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                throw new AppException("Email format is invalid");
            }
            
            if (_db.Customers.Any(x => x.Email == _user.Email))
                throw new AppException("Email is taken");
            string pass = GetMD5(_user.Password);
            _user.Password = pass;
            _db.Customers.Add(_user);
            _db.SaveChanges();
            return _user;
        }
    }
}
