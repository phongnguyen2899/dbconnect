using MISA.Common.Models;
using MISA.DataAccess.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;

namespace MISA.DataAccess.DatabaseAccess
{
    public class DatabaseContext<T> : IDisposable, IDatabaseContext<T>
    {
        #region DECLARE
        readonly string _connectionString = "User Id=nvmanh;Password=12345678@Abc;Host=35.194.135.168;Port=3306;Database=MISACukCuk;Character Set=utf8";
        MySqlConnection _sqlConnection;
        MySqlCommand _sqlCommand;
        #endregion

        #region CONSTRUCTOR
        public DatabaseContext()
        {
            // Khởi tạo kết nối:
            _sqlConnection = new MySqlConnection(_connectionString);
            _sqlConnection.Open();
            // Đối tượng xử lý command:
            _sqlCommand = _sqlConnection.CreateCommand();
            _sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        }
        #endregion

        #region METHOD
        public int DeleteById(object id)
        {
            var entityName = typeof(T).Name;
            _sqlCommand.Parameters.Clear();
            _sqlCommand.CommandText = $"Proc_Delete{entityName}ById";
            _sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                _sqlConnection.Open();
            //_sqlCommand.Parameters.AddWithValue($"@{entityName}Id", id);
            MySqlCommandBuilder.DeriveParameters(_sqlCommand);
            if (_sqlCommand.Parameters.Count > 0)
            {
                _sqlCommand.Parameters[0].Value = id;
            }
            var affectRows = _sqlCommand.ExecuteNonQuery();
            return affectRows;
        }

        public T GetById(object objectId)
        {
            var className = typeof(T).Name;
            _sqlCommand.CommandText = $"Proc_Get{className}ById";
            _sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                _sqlConnection.Open();
            _sqlCommand.Parameters.AddWithValue($"@{className}Id", objectId);
            MySqlDataReader mySqlDataReader = _sqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                var employee = Activator.CreateInstance<T>();
                for (int i = 0; i < mySqlDataReader.FieldCount; i++)
                {
                    var columnName = mySqlDataReader.GetName(i);
                    var value = mySqlDataReader.GetValue(i);
                    var propertyInfo = employee.GetType().GetProperty(columnName);
                    if (propertyInfo != null && value != DBNull.Value)
                    {
                        if (propertyInfo.PropertyType == typeof(Boolean))
                            propertyInfo.SetValue(employee, Convert.ToBoolean(value));
                        else
                            propertyInfo.SetValue(employee, value);
                    }
                }
                return employee;
            }
            return default;
        }

        public IEnumerable<T> Get()
        {
            var employees = new List<T>();
            var className = typeof(T).Name;
            _sqlCommand.CommandText = $"Proc_Get{className}s";
            _sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                _sqlConnection.Open();
            // Thực hiện đọc dữ liệu:
            MySqlDataReader mySqlDataReader = _sqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                var entity = Activator.CreateInstance<T>();
                //employee.EmployeeId = mySqlDataReader.GetGuid(0);
                //employee.EmployeeCode = mySqlDataReader.GetString(1);
                //employee.FullName = mySqlDataReader.GetString(2);

                for (int i = 0; i < mySqlDataReader.FieldCount; i++)
                {
                    var columnName = mySqlDataReader.GetName(i);
                    var value = mySqlDataReader.GetValue(i);
                    var propertyInfo = entity.GetType().GetProperty(columnName);
                    if (propertyInfo != null && value != DBNull.Value)
                    {
                        if (propertyInfo.PropertyType == typeof(Boolean))
                            propertyInfo.SetValue(entity, Convert.ToBoolean(value));
                        else
                            propertyInfo.SetValue(entity, value);
                    }

                }
                employees.Add(entity);
            }
            // 1. Kết nối với Database:
            // 2. Thực thi command lấy dữ liệu:
            // Trả về:
            return employees;
        }

        public int Insert(T entity)
        {
            var entityName = typeof(T).Name;
            _sqlCommand.Parameters.Clear();
            _sqlCommand.CommandText = $"Proc_Insert{entityName}";
            _sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                _sqlConnection.Open();
            MySqlCommandBuilder.DeriveParameters(_sqlCommand);
            var parameters = _sqlCommand.Parameters;
            var properties = typeof(T).GetProperties();
            // Cách 1:
            //foreach (var property in properties)
            //{
            //    var propertyName = property.Name;
            //    var propertyValue = property.GetValue(entity);
            //    foreach (MySqlParameter param in parameters)
            //    {
            //        var paramName = param.ParameterName;
            //        if (paramName == $"@{propertyName}")
            //            param.Value = propertyValue;
            //    }
            //}

            // Cách 2:
            foreach (MySqlParameter param in parameters)
            {
                var paramName = param.ParameterName.Replace("@", string.Empty);
                var property = entity.GetType().GetProperty(paramName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property != null)
                    param.Value = property.GetValue(entity);
            }
            var affectRows = _sqlCommand.ExecuteNonQuery();
            return affectRows;
        }

        public int Update(T entity)
        {
            var entityName = typeof(T).Name;
            _sqlCommand.Parameters.Clear();
            _sqlCommand.CommandText = $"Proc_Update{entityName}";
            _sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                _sqlConnection.Open();
            MySqlCommandBuilder.DeriveParameters(_sqlCommand);
            var parameters = _sqlCommand.Parameters;

            // Cách 2:
            foreach (MySqlParameter param in parameters)
            {
                var paramName = param.ParameterName.Replace("@", string.Empty);
                var property = entity.GetType().GetProperty(paramName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property != null)
                    param.Value = property.GetValue(entity);
            }
            var affectRows = _sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();
            return affectRows;
        }

        public void Dispose()
        {
            _sqlConnection.Close();
        }

        public IEnumerable<T> Get(string storeName)
        {
            var entities = new List<T>();
            _sqlCommand.CommandText = storeName;
            // Thực hiện đọc dữ liệu:
            MySqlDataReader mySqlDataReader = _sqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                var entity = Activator.CreateInstance<T>();
                //employee.EmployeeId = mySqlDataReader.GetGuid(0);
                //employee.EmployeeCode = mySqlDataReader.GetString(1);
                //employee.FullName = mySqlDataReader.GetString(2);

                for (int i = 0; i < mySqlDataReader.FieldCount; i++)
                {
                    var columnName = mySqlDataReader.GetName(i);
                    var value = mySqlDataReader.GetValue(i);
                    var propertyInfo = entity.GetType().GetProperty(columnName);
                    if (propertyInfo != null && value != DBNull.Value)
                        propertyInfo.SetValue(entity, value);
                }
                entities.Add(entity);
            }
            // 1. Kết nối với Database:
            // 2. Thực thi command lấy dữ liệu:
            // Trả về:
            return entities;
        }

        public object Get(string storeName, string code)
        {
            _sqlCommand.Parameters.Clear();
            _sqlCommand.CommandText = storeName;
            _sqlCommand.Parameters.AddWithValue("@EmployeeCode", code);
            // Thực hiện đọc dữ liệu:
            return _sqlCommand.ExecuteScalar();
        }

        /// <summary>
        /// Check trùng bản ghi
        /// </summary>
        /// <param name="entity">đối tượng check</param>
        /// <param name="property">Thông tin check</param>
        /// <returns>true nếu trùng, false nếu không có bản ghi trùng lặp</returns>
        /// CreatedBy: NVMANH (17/11/2020)
        public bool CheckDuplicate(T entity, PropertyInfo property, bool isAddNew)
        {
            var isDuplicate = false;
            var propertyName = property.Name;
            var propertyValue = property.GetValue(entity);
            var entityName = entity.GetType().Name;

            // Lấy khóa chính:
            var keyProperty = typeof(T)
                        .GetProperties()
                        .Where(
                            p => Attribute.IsDefined(property, typeof(KeyAttribute)))
                        .FirstOrDefault();

            // Không gán khóa chính thì lấy trường có hậu tố là Id:
            if (keyProperty == null)
                keyProperty = typeof(T).GetProperty($"{entityName}Id");

            if (_sqlConnection.State == System.Data.ConnectionState.Closed)
                _sqlConnection.Open();
            _sqlCommand.Parameters.Clear();
            if (isAddNew == true)
                _sqlCommand.CommandText = $"SELECT {keyProperty.Name}, {propertyName} FROM {typeof(T).Name} E WHERE E.{propertyName} = @{propertyName} LIMIT 1";
            else
                _sqlCommand.CommandText = $"SELECT {keyProperty.Name}, {propertyName} FROM {typeof(T).Name} E WHERE E.{propertyName} = @{propertyName} AND E.{keyProperty.Name}<> @{keyProperty.Name} LIMIT 1";
            _sqlCommand.CommandType = System.Data.CommandType.Text;
            _sqlCommand.Parameters.AddWithValue($"@{propertyName}", propertyValue);
            if(isAddNew == false)
            {
                _sqlCommand.Parameters.AddWithValue($"@{keyProperty.Name}", keyProperty.GetValue(entity));
            }
            MySqlDataReader mySqlDataReader = _sqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                    isDuplicate = true;
                //else
                //{
                //    var keyValue = mySqlDataReader.GetValue(mySqlDataReader.GetOrdinal($"{keyProperty.Name}"));
                //    if (keyValue.ToString() != keyProperty.GetValue(entity).ToString())
                //        isDuplicate = true;
                //}
            }
            _sqlConnection.Close();
            return isDuplicate;
        }

        #endregion
    }
}
