using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagement.WebAPI.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public float AccountBalance { get; set; }
        public virtual Currency Currencies { get; set; }
        public virtual Role Roles { get; set; }
    }
}
