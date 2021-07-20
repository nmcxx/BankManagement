using BankManagement.WebAPI.Entities;
using BankManagement.WebAPI.Helpers;
using ProductManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankManagement.WebAPI.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(int id);
        Customer Add(Customer model);
        Customer Edit(Customer model);
        void Delete(int id);
    }
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _db;
        public CustomerService(DataContext db)
        {
            _db = db;
        }

        public Customer Add(Customer model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
                throw new AppException("Email is required");
            if (string.IsNullOrWhiteSpace(model.CustomerName))
                throw new AppException("Customer name is required");
            if (string.IsNullOrWhiteSpace(model.Address))
                throw new AppException("Address is required");
            if (string.IsNullOrWhiteSpace(model.PhoneNumber))
                throw new AppException("PhoneNumber is required");

            DateTime DOB = DateTime.Now;
            if (string.IsNullOrWhiteSpace(DOB.ToString()))
                throw new AppException("Date of birth is required");
            bool valid = IsValidEmail(model.Email);
            if (valid == false)
                throw new AppException("Email is invalid");
            if (_db.Customers.Any(x => x.Email == model.Email))
                throw new AppException("Email is taken");        
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
                Roles = _db.Roles.Find(2)                
            };
            _db.Customers.Add(cus);
            try
            {
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                throw new AppException(e.Message);
            }
            return cus;

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
        public void Delete(int id)
        {
            var x = _db.Customers.Find(id);
            if (x == null)
            {
                throw new AppException("Customer id: " + id + "" + "is not found");
            }
            _db.Customers.Remove(x);
        }

        public Customer Edit(Customer model)
        {
            var obj = _db.Customers.Find(model.CustomerId);
            if (obj == null)
                throw new AppException("Customer is not found");
            if (!string.IsNullOrWhiteSpace(model.CustomerName))
                obj.CustomerName = model.CustomerName;
            if (!string.IsNullOrWhiteSpace(model.Address))
                obj.Address = model.Address;
            if (!string.IsNullOrWhiteSpace(model.PhoneNumber))
                obj.PhoneNumber = model.PhoneNumber;
            string DOB = Convert.ToString(model.DateOfBirth);
            if (!string.IsNullOrWhiteSpace(DOB))
                obj.DateOfBirth = model.DateOfBirth;
            obj.Roles = _db.Roles.Find(2);
            _db.Customers.Update(obj);
            _db.SaveChanges();
            return obj;
        }

        public IEnumerable<Customer> GetAll()
        {
            var x = _db.Customers.Select(s => new Customer
            {
                CustomerId = s.CustomerId,
                CustomerName = s.CustomerName,
                Email = s.Email,
                Password =s.Password,
                Address = s.Address,
                PhoneNumber = s.PhoneNumber,
                AccountNumber = s.AccountNumber,
                AccountBalancce =s.AccountBalancce,
                DateOfBirth = s.DateOfBirth,
                Roles = _db.Roles.Where(x => x.RoleId == s.Roles.RoleId).FirstOrDefault()
            }).ToList();
            if(x == null)
            {
                throw new AppException("Data customer is null");
            }
            return x;
        }

        public Customer GetById(int id)
        {
            var x = _db.Customers.Find(id);
            if (x == null)
            {
                throw new AppException("Customer id: " + id + "" + "is not found");
            }
            return x;
        }
    }
}
