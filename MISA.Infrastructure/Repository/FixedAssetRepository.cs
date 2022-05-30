using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Respositories;
using MySqlConnector;
using System.Text.RegularExpressions;

namespace MISA.Infrastructure.Repository
{
    public class FixedAssetRepository : BaseRepository<FixedAsset>, IFixedAssetRepository
    {

        public FixedAssetRepository(IConfiguration configuration) : base(configuration)
        {

        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="fixedAssetId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int DeleteMulti(Guid[] fixedAssetIdList)
        {
            // Tạo dạy danh dách id cần xóa để truyền vào câu lệnh sql
            var idList = "";
            foreach (var item in fixedAssetIdList)
            {
                idList += $"'{item}',";
            }
            idList = idList.Remove(idList.Length - 1, 1);
            var sqlQuerry = $"DELETE FROM FixedAsset WHERE FixedAssetId IN ({idList})";
            var res = _sqlConnection.Execute(sqlQuerry);
            return res;
        }

        /// <summary>
        /// Xử lí tạo ra mã mới
        /// </summary>
        /// <returns></returns>
        public string GetNewFixedAssetCode()
        {
            var newCode = "";
            // Khởi tạo câu truy vấn lấy giá trị fixedAssetCode gần nhất (theo thời gian tạo): 
            var sqlQuery = "SELECT FixedAssetCode FROM FixedAsset ORDER BY CreatedDate DESC";

            // Thực hiện truy vấn lấy về mã gần nhất: 
            var lastCode = _sqlConnection.QueryFirstOrDefault<String>(sqlQuery);

            if (string.IsNullOrEmpty(lastCode))
            {
                newCode = "TS00001";
            }
            else
            {
                // Lấy ra tất cả các số ở cuối:
                var match = new Regex(@"(\d+)*$").Match(lastCode).Value;

                // Nếu không có số ở cuối thì tạo số bắt đầu từ 1:
                if (match == "" || match == null)
                {
                    newCode = lastCode + 1;
                }
                // Nếu đã có số rồi thì cộng thêm 1: 
                else
                {
                    // Thay phần số (nếu có) thành rỗng:
                    var restPart = lastCode.Replace(match, "");
                    // Tăng phần số lên 1 đơn vị: 
                    var numberPart = (int.Parse(match) + 1).ToString($"D{match.Length}");
                    // Ghép chuỗi:
                    newCode = restPart + numberPart;
                }
            }
            return newCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="FixedAssetFilter"></param>
        /// <param name="FixedAssetCategoryName"></param>
        /// <param name="DepartmentName"></param>
        /// <returns></returns>
        public List<FixedAsset> Filter( string? FixedAssetCategoryName, string? DepartmentName,string FixedAssetFilter , int pageIndex , int pageSize )
        {
            // Nếu trả về null thì gắn vào giá trị "":
            if(string.IsNullOrEmpty (FixedAssetFilter) )
            {
                FixedAssetFilter = "";
            }
            // Tính giá trị tổng offset:
            var totalOffset = (pageIndex - 1) * pageSize;
            // Khởi tạo câu truy vấn: 
            var sqlQuery = "SELECT * FROM FixedAsset WHERE (FixedAssetName LIKE CONCAT('%',@FixedAssetFilter,'%') OR FixedAssetCode LIKE CONCAT('%',@FixedAssetFilter,'%')) ";

            if (!string.IsNullOrEmpty(FixedAssetCategoryName))
            {
                sqlQuery += "AND FixedAssetCategoryName = @FixedAssetCategoryName ";
            }

            if (!string.IsNullOrEmpty(DepartmentName))
            {
                sqlQuery += "AND DepartmentName = @DepartmentName ";
            }

            sqlQuery += "ORDER BY CreatedDate DESC LIMIT @pageSize OFFSET @totalOffset";
            var parameters = new DynamicParameters();
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@totalOffset", totalOffset);
            parameters.Add("@FixedAssetFilter", FixedAssetFilter);
            parameters.Add("@FixedAssetCategoryName", FixedAssetCategoryName);
            parameters.Add("@DepartmentName", DepartmentName);

            // Thực hiện truy vấn: 
            var FilterList = _sqlConnection.Query<FixedAsset>(sqlQuery, param: parameters);


            return FilterList.ToList();
        }

        /// <summary>
        /// Lấy tổng số bản ghi tìm được
        /// </summary>
        /// <param name="FixedAssetCategoryName"></param>
        /// <param name="DepartmentName"></param>
        /// <param name="FixedAssetFilter"></param>
        /// <returns></returns>
        public int GetFixedAssetCount(string? FixedAssetCategoryName, string? DepartmentName, string FixedAssetFilter)
        {
            // Nếu trả về null thì gắn vào giá trị "":
            if (string.IsNullOrEmpty(FixedAssetFilter))
            {
                FixedAssetFilter = "";
            }
            // Khởi tạo câu truy vấn: 
            var sqlQuery = "SELECT * FROM FixedAsset WHERE (FixedAssetName LIKE CONCAT('%',@FixedAssetFilter,'%') OR FixedAssetCode LIKE CONCAT('%',@FixedAssetFilter,'%')) ";

            if (!string.IsNullOrEmpty(FixedAssetCategoryName))
            {
                sqlQuery += "AND FixedAssetCategoryName = @FixedAssetCategoryName ";
            }

            if (!string.IsNullOrEmpty(DepartmentName))
            {
                sqlQuery += "AND DepartmentName = @DepartmentName ";
            }
            var parameters = new DynamicParameters();
    
            parameters.Add("@FixedAssetFilter", FixedAssetFilter);
            parameters.Add("@FixedAssetCategoryName", FixedAssetCategoryName);
            parameters.Add("@DepartmentName", DepartmentName);

            // Thực hiện truy vấn: 
            var ListCount = _sqlConnection.Query<FixedAsset>(sqlQuery, param: parameters);


            return ListCount.Count();
        }
    }
}
