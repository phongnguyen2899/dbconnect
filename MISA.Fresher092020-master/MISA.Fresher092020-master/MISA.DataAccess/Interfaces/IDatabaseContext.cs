using MISA.Common.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MISA.DataAccess.Interfaces
{
    public interface IDatabaseContext<T>
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(string storeName);
        object Get(string storeName, string code);
        T GetById(object employeeId);
        int Insert(T employee);
        int Update(T employee);
        int DeleteById(object id);
        bool CheckDuplicate(T entity, PropertyInfo property, bool isAddNew);
    }
}
