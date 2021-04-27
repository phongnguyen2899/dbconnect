using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Bussiness.Interfaces;
using MISA.Common.Models;

namespace MISA.CukCuk.Api
{
    [Route("api/CustomerGroups")]
    [ApiController]
    public class CustomerGroupsApi : BaseApi<CustomerGroup>
    {
        IBaseService<CustomerGroup> _service;
        public CustomerGroupsApi(IBaseService<CustomerGroup> service) : base(service)
        {
            _service = service;
        }
    }
}
