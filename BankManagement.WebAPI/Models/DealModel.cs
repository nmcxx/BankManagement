using BankManagement.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagement.WebAPI.Models
{
    public class DealModel
    {
        public float Money { get; set; }
        public DateTime Date { get; set; }
        public int CustomerIdSend { get; set; }
        public int CustomerIdRevice { get; set; }
        public virtual Service Services { get; set; }
        public virtual Currency Currencies { get; set; }

    }
}
