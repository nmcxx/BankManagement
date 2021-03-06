using AutoMapper;
using BankManagement.WebAPI.Entities;
using BankManagement.WebAPI.Models;
using ProductManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LoginModel, Customer>();
            CreateMap<CustomerModel, Customer>();
            CreateMap<Customer, CustomerModel>();
            CreateMap<Deal, DealModel>();
        }
    }
}
