using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MISA.DataAccess.Interfaces
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Lấy dữ liệu
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NVMANH (15/10/2020)
        IEnumerable<T> Get();
        T GetById(Guid employeeId);
        int Insert(T employee);
        int Update(T employee);
        int Delete(object id);
        bool CheckDuplicate(T entity,PropertyInfo property, bool isAddNew = true);
    }
}
