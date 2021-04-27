using MISA.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Bussiness.Interfaces
{
    public interface IBaseService<T>
    {
        /// <summary>
        /// Lấy dữ liệu
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NVMANH (15/10/2020)
        IEnumerable<T> Get();

        T GetById(Guid employeeId);
        ServiceResponse Insert(T employee);
        ServiceResponse Update(T employee);
        int Delete(object id);
    }
}
