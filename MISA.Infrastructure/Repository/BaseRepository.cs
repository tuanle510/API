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
        /// <returns> Lấy tất cả bản ghi </returns>
        public List<T> Get()
        {
            // Thực hiện khai báo câu lệnh truy vấn SQL:
            var sqlCommand = $"SELECT * FROM {_tableName} ORDER BY CreatedDate DESC";

            // Thực hiện câu truy vấn:
            var entities = _sqlConnection.Query<T>(sqlCommand);

            // Trả về dữ liệu dạng List:
            return entities.ToList();
        }

        /// <summary>
        /// Xử lí lấy dữ liệu theo ID
        /// </summary>
        /// <param name="entityId"> id của dữ liện cần lấy </param>
        /// <returns> Đối tượng lấy về theo id </returns>
        public T GetById(Guid entityId)
        {
            // Thực hiện khai báo câu lệnh truy vấn SQL:
            var sqlQuery = $"SELECT * FROM {_tableName} WHERE {_tableName}Id = @entityId";
            var parameters = new DynamicParameters();
            parameters.Add("@entityId", entityId);

            // Thực hiện câu truy vấn:
            var entities = _sqlConnection.QueryFirstOrDefault<T>(sqlQuery, param: parameters);

            // Trả về dữ liệu dạng List:
            return entities;
        }

        /// <summary>
        /// Xử lí thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns> Số lượng bản ghi đã được thêm </returns>
        public int Insert(T entity)
        {
            // Khỏi tạo câu lệnh 
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
            // Xóa dấu phẩy cuối cùng của chuỗi
            columnNames = columnNames.Remove(columnNames.Length - 1, 1);
            columnParams = columnParams.Remove(columnParams.Length - 1, 1);
            var sqlCommand = $"INSERT INTO {_tableName}({columnNames}) VALUES ({columnParams})";
            var rowAffects = _sqlConnection.Execute(sqlCommand, param: entity);
            return rowAffects;
        }


        /// <summary>
        /// Xử lí xóa 1 dối tượng theo id 
        /// </summary>
        /// <param name="entityId"> id của đối tượng cần xóa</param>
        /// <returns> số lượng bản ghi đã được xóa </returns>
        public int Delete(Guid entityId)
        {
            // Khỏi tạo câu truy vấn:
            var sqlQuery = $"DELETE FROM {_tableName} WHERE {_tableName}Id = @entityId";
            var parameters = new DynamicParameters();
            parameters.Add("@entityId", entityId);
            // Thực hiện câu truy vấn: 
            var res = _sqlConnection.Execute(sqlQuery, param: parameters);
            return res;
        }


        /// <summary>
        /// Xủ lí sửa 1 đối tượng theo id
        /// </summary>
        /// <param name="entityId"> id đối tượng cần xóa </param>
        /// <param name="entity"> bản ghi đã được sửa </param>
        /// <returns> số lượng bản ghi đã được sửa  </returns>
        public int Update(Guid entityId, T entity)
        {
            var setParams = "";
            var parameter = new DynamicParameters();
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

                var isPrimarykey = prop.IsDefined(typeof(PrimaryKey), true);
                if (isPrimarykey == true && propType == typeof(Guid))
                {
                    prop.SetValue(entity, entityId);
                }
                setParams += $"{propName} = @{propName},";
                parameter.Add($"@{propName}", propValue);
            }
            // Xóa dấu phẩy cuối cùng của chuỗi
            setParams = setParams.Remove(setParams.Length - 1, 1);
            var sqlCommand = $"UPDATE {_tableName} SET {setParams} WHERE  {_tableName}Id = @entityId";
            parameter.Add("@entityId", entityId);
            var rowAffects = _sqlConnection.Execute(sqlCommand, param: parameter);
            return rowAffects;
        }


        /// <summary>
        /// Kiểm tra code của đối tượng có bị trùng không
        /// </summary>
        /// <param name="entityCode"> Code đối tượng </param>
        /// <param name="entityId"> Thêm mới thì tryền vào Guid.Empty , Sửa thì truyền vào Id của đối tượng </param>
        /// <returns> true - đã bị trùng, false - không trùng </returns>
        public bool CheckCodeExist(string entityCode, Guid? entityId)
        {
            // Khởi tạo câu lệnh:
            var sqlQueryCheckDuplicateCode = $"SELECT {_tableName}Code FROM {_tableName} WHERE {_tableName}Code=@{_tableName}Code AND {_tableName}Id <> @{_tableName}Id";
            // Kiểm tra xem mã đã tồn tại hay chưa?
            var parametersDup = new DynamicParameters();
            parametersDup.Add($"@{_tableName}Code", entityCode);
            parametersDup.Add($"@{_tableName}Id", entityId);
            var codeDuplicate = _sqlConnection.QueryFirstOrDefault<string>(sqlQueryCheckDuplicateCode, param: parametersDup);

            if (codeDuplicate != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
