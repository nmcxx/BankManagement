using System;
using System.Collections.Generic;
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
        public int PhoneNumber { get; set; }
        public string AccountNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public float AccountBalancce { get; set; }
        public virtual ICollection<Role> Roles { get; set; }


    }
}
