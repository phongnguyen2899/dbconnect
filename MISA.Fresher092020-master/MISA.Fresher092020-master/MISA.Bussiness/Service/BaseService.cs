using MISA.Bussiness.Interfaces;
using MISA.Common.Models;
using MISA.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MISA.Bussiness.Service
{
    public class BaseService<T> : IBaseService<T>
    {
        IBaseRepository<T> _baseRepository;
        protected List<string> validateErrorResponseMsg = new List<string>();
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public int Delete(object id)
        {
            return _baseRepository.Delete(id);
        }

        public IEnumerable<T> Get()
        {
            return _baseRepository.Get();
        }

        public T GetById(Guid objectId)
        {
            return _baseRepository.GetById(objectId);
        }

        public ServiceResponse Insert(T entity)
        {
            var serviceResponse = new ServiceResponse();
            if (Validate(entity) == true)
            {
                serviceResponse.Success = true;
                serviceResponse.Msg.Add("Thành công");
                serviceResponse.Data = _baseRepository.Insert(entity);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Msg = validateErrorResponseMsg;
            }
            return serviceResponse;
        }


        public ServiceResponse Update(T entity)
        {
            var serviceResponse = new ServiceResponse();
            if (Validate(entity, false) == true)
            {
                serviceResponse.Success = true;
                serviceResponse.Msg.Add("Thành công");
                serviceResponse.Data = _baseRepository.Update(entity);
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Msg = validateErrorResponseMsg;
            }

            return serviceResponse;
        }

        protected virtual bool Validate(T entity, bool isAddNew = true)
        {
            // Check trùng mã:
            //- Duyệt tất cả các Property có gán Unique Attribute:
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.IsDefined(typeof(Unique),false))
                {
                    var displayName = property.Name;
                    object[] propertyDisplayName = property.GetCustomAttributes(typeof(DisplayNameAttribute),true);
                    if (propertyDisplayName.Length > 0)
                        displayName = (propertyDisplayName[0] as DisplayNameAttribute).DisplayName;
                    var isDuplicate = _baseRepository.CheckDuplicate(entity,property, isAddNew);
                    if (isDuplicate == true)
                    {
                        validateErrorResponseMsg.Add($"Thông tin [{displayName}] đã tồn tại trên hệ thống");
                    }
                }
            }
            if (validateErrorResponseMsg.Count > 0)
                return false;
            return true;
        }
    }
}
