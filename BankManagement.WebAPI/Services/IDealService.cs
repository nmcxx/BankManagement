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
        IEnumerable<Deal> GetByIDCus(int id);
        Deal Add(Deal model);
        Deal Withdraw(int customerId, int currentcy, float withdrawnumber);
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

        public IEnumerable<Deal> GetAll()
        {
            throw new NotImplementedException();
        }

        public Deal GetByID(int id)
        {
            var x = _db.Deals.Where(x => x.DealId == id).FirstOrDefault();
            if (x == null)
            {
                throw new AppException("Dead id: " + id + "" + "is not found");
            }
            return x;
        }

        public IEnumerable<Deal> GetByIDCus(int id)
        {
            var cus = _db.Customers.Find(id);
           /* if (cus == null)
            {
                throw new Exception("List dead of customer " + cus + " not found");
            }*/
            //var deal = _db.Deals.Where(x => x.Customers.CustomerId == id).ToList();
            var deal = _db.Deals.Where(z=>z.Customers.CustomerId==id).Select(s => new Deal
            {
                DealId = s.DealId,
                Money = s.Money,
                Date = s.Date,
                CustomerIdRevice = s.CustomerIdRevice,
                CustomerIdSend = s.CustomerIdSend,
                Customers = cus,
                Services =_db.Services.Where(a=>a.ServiceId == s.Services.ServiceId).FirstOrDefault(),
                Currencies = _db.Currencies.Where(a => a.CurrencyId == s.Currencies.CurrencyId).FirstOrDefault(),
            }).ToList();

            return deal;
        }

        public Deal Withdraw(int customerId, int currentcy, float withdrawnumber)
        {
            var model = _db.Customers.Find(customerId);

            if (_db.Customers.Any(x => x.CurrencyId != currentcy))
                throw new AppException("Currentcy " + currentcy + " is not valid");
            if (_db.Customers.Any(x => x.AccountBalancce < withdrawnumber))
                throw new AppException("Account balance " + withdrawnumber + " is not enought");
            if (withdrawnumber % 50000 != 0)
                throw new AppException(withdrawnumber + " Withdraw number must multiplicity of 50000");
            if (withdrawnumber < 50000 )
                throw new AppException(withdrawnumber + " Must > 50000");

            var deal = new Deal
            {
                Money = withdrawnumber,
                Date = DateTime.Now,
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
