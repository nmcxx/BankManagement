using BankManagement.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagement.WebAPI.Services
{
    public interface IDealService
    {
        IEnumerable<Deal> GetAll();
        Deal GetByID(int id);
        Deal Add(Deal model);
        void Delete(int id);
    }
    public class DealService : IDealService
    {
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
    }
}
