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
        Deal Withdraw(int customerId, int currentcy, float withdrawnumber);
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
            if (_db.Customers.Find(model.CustomerIdSend) == null || _db.Customers.Find(model.CustomerIdRevice) == null)
                throw new AppException("Customer is not exists");

            if (_db.Customers.Find(model.CustomerIdSend).AccountBalancce - model.Money <= 50000)
                throw new AppException("Your amount is not enough");


            try
            {
                _db.Customers.Find(model.CustomerIdSend).AccountBalancce -= model.Money;
                _db.Customers.Find(model.CustomerIdRevice).AccountBalancce += model.Money;
                model.Date = DateTime.Now;
                var deal = _db.AddAsync(model);
                _db.SaveChangesAsync();
                return deal.Result.Entity;
                
            }
            catch(Exception e)
            {
                throw e;
            }


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
        public Deal Withdraw(int customerId, int currentcy, float withdrawnumber)
        {
            var model = _db.Customers.Find(customerId);
            System.ComponentModel.DateTimeConverter c = new System.ComponentModel.DateTimeConverter();

            if (_db.Customers.Any(x => x.CustomerId != currentcy))
                throw new AppException("Currentcy " + currentcy + " is not valid");
            if (_db.Customers.Any(x => x.AccountBalancce < withdrawnumber))
                throw new AppException("Account balance " + withdrawnumber + " is not enought");
            var deal = new Deal
            {
                Money = withdrawnumber,
                Date = (DateTime)c.ConvertFromString("yyyy-mm-dd"),
                CustomerIdRevice = model.CustomerId,
                CustomerIdSend = 0,
                Customers = _db.Customers.Where(x => x.CustomerId == model.CustomerId).FirstOrDefault(),
                Services = _db.Services.Where(v => v.ServiceId == 2).FirstOrDefault(),
                Currencies = _db.Currencies.Where(w => w.CurrencyId == currentcy).FirstOrDefault()
            };
            model.AccountBalancce -= deal.Money;
            _db.Customers.Update(model);
            _db.Deals.Add(deal);
            _db.SaveChanges();
            return deal;
        }
    }
}
