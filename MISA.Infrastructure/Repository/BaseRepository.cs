using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Interfaces.Respositories;
using MISA.Core.MISAAttribute;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        /// <summary>
        /// Xử lí kết nối với database 
        /// </summary>
        IConfiguration _configuration;
        readonly string _connectionString = string.Empty;
        protected MySqlConnection _sqlConnection;
        string _tableName;
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            // Khai báo thông tin database:
            _connectionString = _configuration.GetConnectionString("LTTUAN");
            // Khỏi tạo kết nối đến database --> sử dụng mySqlConnector
            _sqlConnection = new MySqlConnection(_connectionString);
            _tableName = typeof(T).Name;
        }

        /// <summary>
        /// Xử lí lấy dữ liệu 
        /// </summary>
        /// <returns></returns>
        public List<T> Get()
        {
            // Thực hiện khai báo câu lệnh truy vấn SQL:
            var sqlCommand = $"SELECT * FROM {_tableName}";

            // Thực hiện câu truy vấn:
            var entities = _sqlConnection.Query<T>(sqlCommand);

            // Trả về dữ liệu dạng List:
            return entities.ToList();
        }
        
        /// <summary>
        /// Xử lí thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(T entity)
        {
            var columnNames = "";
            var columnParams = "";
            // Lấy ra tất cả các properties của class:
            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                // Tên của prop:
                var propName = prop.Name;
                // Giá trị của prop:
                var propValue = prop.GetValue(entity);
                // Kiểu dữ liệu của prop:
                var propType = prop.PropertyType;
                // Kiểm tra prop hiện tại có phải là khóa chính hay không, nếu đúng thì gán lại giá trị mới cho prop:
                var isPrimarykey = prop.IsDefined(typeof(PrimaryKey), true);
                if (isPrimarykey == true && propType == typeof(Guid))
                {
                    prop.SetValue(entity, Guid.NewGuid());
                }
                // Bồ sung cột hiện tại vào chuỗi câu truy vấn cột dữ liệu:
                columnNames += $" {propName},";
                columnParams += $"@{propName},";
            }
            columnNames = columnNames.Remove(columnNames.Length - 1, 1);
            columnParams = columnParams.Remove(columnParams.Length - 1, 1);
            var sqlCommand = $"INSERT INTO {_tableName}({columnNames}) VALUES ({columnParams})";
            var rowAffects = _sqlConnection.Execute(sqlCommand, param: entity);
            return rowAffects;
        }

        public int Delete(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public int Update(Guid entityId, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
