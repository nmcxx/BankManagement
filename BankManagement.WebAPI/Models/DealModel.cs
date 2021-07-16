using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagement.WebAPI.Models
{
    public class DealModel
    {
        public int Money { get; set; }
        public DateTime Date { get; set; }
        public int CustomerIdSend { get; set; }
        public int CustomerIdRevice { get; set; }
        public int  ServicesID { get; set; }
        public int CurrencyID  { get; set; }
    }
}
