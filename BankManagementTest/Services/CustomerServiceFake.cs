using BankManagement.WebAPI.Entities;
using BankManagement.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using BankManagement.WebAPI.Helpers;
using System.Linq;
using ProductManagement.Helpers;
using System.Text.RegularExpressions;
using System.Globalization;

namespace BankManagementTest.Services
{
    public class CustomerServiceFake : ICustomerService
    {
        List<Customer> _db;
        public CustomerServiceFake()
        {
            _db = new List<Customer>()
            {
                new Customer(){CustomerId=1, CustomerName="duy",Email="duy@gmail.com",Password="ABCD1234",Address="Quy Nhon", PhoneNumber="0112345678",AccountNumber="58010001234",
                    AccountBalancce=1000000, CurrencyId=1, DateOfBirth=DateTime.Parse("01/01/1999"),Roles = new Role{RoleId=2,RoleName="customer"} },
                 new Customer(){CustomerId=2, CustomerName="trung",Email="trung@gmail.com",Password="EFGH1234",Address="Quy Nhon", PhoneNumber="0123654789",AccountNumber="58010001543",
                    AccountBalancce=1000000, CurrencyId=1, DateOfBirth=DateTime.Parse("03/03/1998"),Roles = new Role{RoleId=2,RoleName="customer"} },
                 new Customer(){CustomerId=3, CustomerName="can",Email="can@gmail.com",Password="ZXCD1234",Address="Quy Nhon", PhoneNumber="0789456123",AccountNumber="5801000187",
                    AccountBalancce=1000000, CurrencyId=1, DateOfBirth=DateTime.Parse("04/04/1999"),Roles = new Role{RoleId=2,RoleName="customer"} }
            };
        }
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public Customer Add(Customer model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
                throw new Exception("Email is required");
            if (string.IsNullOrWhiteSpace(model.CustomerName))
                throw new Exception("Customer name is required");
            if (string.IsNullOrWhiteSpace(model.Address))
                throw new Exception("Address is required");
            if (string.IsNullOrWhiteSpace(model.PhoneNumber))
                throw new Exception("PhoneNumber is required");
            DateTime DOB = (DateTime)DateManager.GetDate(model.DateOfBirth.ToString());
            if (string.IsNullOrWhiteSpace(DOB.ToString()))
                throw new Exception("Date of birth is required");
            bool valid = IsValidEmail(model.Email);
            if (valid == false)
                throw new Exception("Email is invalid");
            if (_db.Any(x => x.Email == model.Email))
                throw new Exception("Email is taken");
            StringBuilder builderPass = new StringBuilder();
            builderPass.Append(RandomString(4, false));
            builderPass.Append(RandomNumber(1000, 9999));
            string pass = builderPass.ToString();
            model.Password = pass;

            StringBuilder builderAC = new StringBuilder();
            builderAC.Append(RandomNumber(1000, 9999));
            string AC = "58010000" + builderAC.ToString();
            model.AccountNumber = AC;

            var cus = new Customer
            {
                CustomerName = model.CustomerName,
                Email = model.Email,
                Password = model.Password,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                AccountNumber = model.AccountNumber,
                AccountBalancce = model.AccountBalancce,
                DateOfBirth = model.DateOfBirth,
                Roles = new Role { RoleId=2, RoleName="customer"}
            };
           _db.Add(cus);
            return cus;
        }

        public void Delete(int id)
        {
            var x = _db.Find(s => s.CustomerId == id);
            if(x == null)
                throw new Exception("Customer is not found");
            _db.Remove(x);
        }

        public Customer Edit(Customer model)
        {
            var x = _db.Find(s => s.CustomerId == model.CustomerId);
            if (x == null)
                throw new Exception("Customer is not found");
            _db.Remove(x);
            x = model;
            _db.Add(x);
            return x;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _db;
        }

        public Customer GetById(int id)
        {
            var x = _db.Find(s => s.CustomerId == id);
            return x;
        }
    }
}
