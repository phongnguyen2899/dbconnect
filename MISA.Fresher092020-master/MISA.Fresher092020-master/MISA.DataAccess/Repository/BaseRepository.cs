using MISA.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MISA.DataAccess.Repository
{
    public class BaseRepository<T>: IBaseRepository<T>
    {
        protected IDatabaseContext<T> _databaseContext;
        public BaseRepository(IDatabaseContext<T> databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool CheckDuplicate(T entity,PropertyInfo property, bool isAddNew = true)
        {
            return _databaseContext.CheckDuplicate(entity, property, isAddNew);
        }

        public int Delete(object id)
        {
            return _databaseContext.DeleteById(id);
        }

        public IEnumerable<T> Get()
        {
            return _databaseContext.Get();
        }

        public T GetById(Guid objectId)
        {
            return _databaseContext.GetById(objectId);
        }

        public int Insert(T entity)
        {
            return _databaseContext.Insert(entity);
        }

        public int Update(T entity)
        {
            return _databaseContext.Update(entity);
        }
    }
}
