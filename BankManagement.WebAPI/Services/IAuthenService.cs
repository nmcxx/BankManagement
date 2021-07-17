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

      
    }
}
