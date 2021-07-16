using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagement.WebAPI.Entities
{
    public class Deal
    {
        public int DealId { get; set; }
        public int Money { get; set; }
        public DateTime Date { get; set; }
        public int CustomerIdRevice { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<Currency> Currencies { get; set; }

    }
}
