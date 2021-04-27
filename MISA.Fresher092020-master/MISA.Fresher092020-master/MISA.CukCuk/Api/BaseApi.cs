using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Bussiness.Interfaces;

namespace MISA.CukCuk.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApi<T> : ControllerBase
    {
        IBaseService<T> _baseService;
        public BaseApi(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }
        // GET: api/<DepartmentApi>
        [HttpGet]
        public IActionResult Get()
        {
            var rs = _baseService.Get();
            if (rs != null)
                return Ok(rs);
            else
                return NoContent();
        }

        /// <summary>
        /// Lấy thông tin nhân viên theo Id
        /// </summary>
        /// <param name="id">Id của nhân viên</param>
        /// <returns></returns>
        /// CreatedBy: NVMANH (13/10/2020)
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] Guid id)
        {
            var employee = _baseService.GetById(id);
            if (employee != null)
                return Ok(employee);
            else
                return NoContent();
        }

        // POST api/<EmployeesApi>
        [HttpPost]
        public IActionResult Post([FromBody] T employee)
        {
            var serviceResponse = _baseService.Insert(employee);
            var affectRows = serviceResponse.Data != null ? ((int)serviceResponse.Data) : 0;
            if ( affectRows > 0)
                return CreatedAtAction("POST", affectRows);
            else
                return BadRequest(serviceResponse);
        }

        // PUT api/<EmployeesApi>/5
        [HttpPut()]
        public IActionResult Put([FromBody] T entity)
        {
            var serviceResponse = _baseService.Update(entity);
            var affectRows = serviceResponse.Data != null ? ((int)serviceResponse.Data) : 0;
            if (affectRows > 0)
                return Ok(affectRows);
            else
                return BadRequest(serviceResponse);
        }

        // DELETE api/<EmployeesApi>/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]string id)
        {
            var affectRows = _baseService.Delete(id);
            return Ok(affectRows);
        }

        [HttpDelete()]
        public IActionResult DeleteMultiRecord([FromQuery] string id)
        {
            var affectRows = _baseService.Delete(id);
            return Ok(affectRows);
        }

    }
}
