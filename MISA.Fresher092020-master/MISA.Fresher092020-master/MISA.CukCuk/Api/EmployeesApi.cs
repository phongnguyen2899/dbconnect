using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Net.WebSockets;
using MISA.DataAccess.Interfaces;
using MISA.Common.Models;
using MISA.Bussiness.Interfaces;
using MISA.Bussiness.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Api
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesApi : BaseApi<Employee>
    {
        IEmployeeService _employeeService;
        public EmployeesApi(IEmployeeService employeeService):base(employeeService)
        {
            _employeeService = employeeService;
        }
    }
}
