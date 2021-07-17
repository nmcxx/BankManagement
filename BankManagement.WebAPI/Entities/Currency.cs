using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagement.WebAPI.Entities
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public int ExchangeRate_id { get; set; }
        public virtual ICollection<ExchangeRate> ExchangeRates { get; set; }
    }
}
