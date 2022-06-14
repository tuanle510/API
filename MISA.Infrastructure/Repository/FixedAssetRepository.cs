﻿using Dapper;
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
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="FixedAssetFilter"></param>
        /// <param name="FixedAssetCategoryName"></param>
        /// <param name="DepartmentName"></param>
        /// <returns></returns>
        public object Filter(string? FixedAssetCategoryName, string? DepartmentName, string FixedAssetFilter, int pageIndex, int pageSize)
        {
            // Nếu trả về null thì gắn vào giá trị "":
            if (string.IsNullOrEmpty(FixedAssetFilter))
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

            var parameters = new DynamicParameters();
            parameters.Add("@FixedAssetFilter", FixedAssetFilter);
            parameters.Add("@FixedAssetCategoryName", FixedAssetCategoryName);
            parameters.Add("@DepartmentName", DepartmentName);
            var count = _sqlConnection.Query<FixedAsset>(sqlQuery, param: parameters);


            sqlQuery += "ORDER BY CreatedDate DESC LIMIT @pageSize OFFSET @totalOffset";
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@totalOffset", totalOffset);

            // Thực hiện truy vấn: 
            var list = _sqlConnection.Query<FixedAsset>(sqlQuery, param: parameters);

            var res = new
            {
                List = (List<FixedAsset>)list.ToList(),
                Count = (int)count.Count()
            };

            // Trả về dữ liệu dạng List:
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fixedAssetList"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public object GetRestFixedAssetList(Guid[] fixedAssetList, int pageIndex, int pageSize)
        {
            var sqlCommand = "";
            // Tính giá trị tổng offset:
            var totalOffset = (pageIndex - 1) * pageSize;
            if (fixedAssetList.Length == 0)
            {
                // Nếu không có list truyền vào:
                sqlCommand = $"SELECT * FROM FixedAsset ORDER BY CreatedDate DESC";
            }
            else
            {
                // Nếu có list truyền vào:
                sqlCommand = $"SELECT * FROM FixedAsset WHERE FixedAssetId NOT IN @fixedAssetList ORDER BY CreatedDate DESC";
            }
            var parameters = new DynamicParameters();
            parameters.Add("@fixedAssetList", fixedAssetList);
            // Thực hiện câu truy vấn:
            var count = _sqlConnection.Query<FixedAsset>(sqlCommand, param: parameters);

            parameters.Add("@pageSize", pageSize);
            parameters.Add("@totalOffset", totalOffset);
            sqlCommand += $" LIMIT @pageSize OFFSET @totalOffset";
            
            var list = _sqlConnection.Query<FixedAsset>(sqlCommand, parameters);
            var res = new
            {
                List = (List<FixedAsset>)list.ToList(),
                Count = (int)count.Count()
            };

            // Trả về dữ liệu dạng List:
            return res;
        }
    }
}
