﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagement.WebAPI.Models
{
    public class CustomerModel
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        [StringLength(10)]
        public int PhoneNumber { get; set; }
        public string AccountNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}