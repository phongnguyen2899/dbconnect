using Microsoft.AspNetCore.Mvc;
using MISA.Bussiness.Interfaces;
using MISA.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerApi:BaseApi<Customer>
    {
        IBaseService<Customer> _service;
        public CustomerApi(IBaseService<Customer> service) : base(service)
        {
            _service = service;
        }
    }
}
