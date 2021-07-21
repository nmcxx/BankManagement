using BankManagement.WebAPI.Entities;
using BankManagement.WebAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using ProductManagement.Helpers;
using System.Globalization;

namespace BankManagement.WebAPI.Services
{
    public interface IDealService
    {
        IEnumerable<Deal> GetAll();
        IEnumerable<Deal> GetByID(int id);
        Deal Withdraw(int customerId, int currentcy, float withdrawnumber);
        void Delete(int id);
        IEnumerable<Deal> SearchByDate(string startDate, string endDate);
    }
    public class DealService : IDealService
    {
        private readonly DataContext _db;
        public DealService(DataContext db)
        {
            _db = db;
        }

        public void Delete(int id)
        {
            var obj = _db.Deals.Find(id);
            if(obj == null)
                throw new AppException("Deal is not found");
            _db.Deals.Remove(obj);
            _db.SaveChanges();
        }

        public IEnumerable<Deal> GetAll()
        {
            var deal = _db.Deals.Select(s => new Deal
            {
                DealId = s.DealId,
                Money = s.Money,
                Date = s.Date,
                CustomerIdRevice = s.CustomerIdRevice,
                CustomerIdSend = s.CustomerIdSend,
                Customers = _db.Customers.Where(v => v.CustomerId == s.Customers.CustomerId).FirstOrDefault(),
                Services = _db.Services.Where(a => a.ServiceId == s.Services.ServiceId).FirstOrDefault(),
                Currencies = _db.Currencies.Where(b => b.CurrencyId == s.Currencies.CurrencyId).FirstOrDefault()
            }).ToList();
            return deal;
        }

        public IEnumerable<Deal> GetByID(int id)
        {
            var customer = _db.Customers.Find(id);

            var deal = _db.Deals.Where(x => x.Customers.CustomerId == customer.CustomerId).Select(s => new Deal {
                DealId =s.DealId,
                Money = s.Money,
                Date = s.Date,
                CustomerIdRevice = s.CustomerIdRevice,
                CustomerIdSend = s.CustomerIdSend,
                Customers = customer,
                Services = _db.Services.Where(a => a.ServiceId == s.Services.ServiceId).FirstOrDefault(),
                Currencies = _db.Currencies.Where(b => b.CurrencyId == s.Currencies.CurrencyId).FirstOrDefault()
            }).ToList();
            return deal;
        }

        public IEnumerable<Deal> SearchByDate(string startDate, string endDate)
        {
            if (startDate == null || endDate == null)
                throw new AppException("date not null");
            DateTime start = DateManager.GetDate(startDate);

            DateTime end = DateManager.GetDate(endDate);

            if(start > end)
                throw new AppException("start date must less than end day");
            var rangeData = _db.Deals.Where(x => x.Date >= start && x.Date <= end)
                .Select(s => new Deal
                {
                    DealId = s.DealId,
                    Money = s.Money,
                    Date = s.Date,
                    CustomerIdRevice =s.CustomerIdRevice,
                    CustomerIdSend =s.CustomerIdSend,
                    Customers = _db.Customers.Where(a => a.CustomerId == s.Customers.CustomerId).FirstOrDefault(),
                    Services = _db.Services.Where(b => b.ServiceId == s.Services.ServiceId).FirstOrDefault(),
                    Currencies =_db.Currencies.Where(c => c.CurrencyId == s.Currencies.CurrencyId).FirstOrDefault()
                })
                .ToList();
            return rangeData;
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
