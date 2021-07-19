using BankManagement.WebAPI.Entities;
using BankManagement.WebAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using ProductManagement.Helpers;

namespace BankManagement.WebAPI.Services
{
    public interface IDealService
    {
        IEnumerable<Deal> GetAll();
        Deal GetByID(int id);
        Deal Add(Deal model);
        Deal Withdraw(string accountnumber, string password, int currentcy, float withdrawnumber);
        void Delete(int id);
    }
    public class DealService : IDealService
    {
        private readonly DataContext _db;
        public DealService(DataContext db)
        {
            _db = db;
        }
        public Deal Add(Deal model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Deal> GetAll()
        {
            throw new NotImplementedException();
        }

        public Deal GetByID(int id)
        {
            throw new NotImplementedException();
        }
        public Deal Withdrawint( int currentcy, float withdrawnumber)
        {
            CustomerService
            var deal = _db.Deals.SingleOrDefault(s => );
            System.ComponentModel.DateTimeConverter c = new System.ComponentModel.DateTimeConverter();

            if (_db.Customers.Any(x => x.CustomerId != currentcy))
                throw new AppException("Currentcy " + currentcy + " is not valid");
            if (_db.Customers.Any(x => x.AccountBalance < withdrawnumber))
                throw new AppException("Account balance " + withdrawnumber + " is not enought");
            deal.AccountNumber = user.AccountNumber;
            deal.Money = withdrawnumber;
            deal.Date = (DateTime)c.ConvertFromString("yyyy-mm-dd");
            deal.Services.ServiceId = 2;
            deal.Currencies.CurrencyId = currentcy;
            user.AccountBalance -= deal.Money;
            _db.Customers.Update(user);
            _db.Deals.Add(deal);
            _db.SaveChanges();

            return deal;
        }
    }
}
