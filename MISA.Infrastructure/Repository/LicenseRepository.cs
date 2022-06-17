using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Respositories;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class LicenseRepository : BaseRepository<License>, ILicenseRepository
    {
        IBaseRepository<LicenseDetail> _baseRepository;

        public LicenseRepository(IConfiguration configuration) : base(configuration)
        {

        }

        /// <summary>
        /// Tạo mới chứng từ và danh sách tài sản có chứng từ:
        /// </summary>
        /// <param name="newLicense"></param>
        /// <returns></returns>
        public int AddLicenseDetail(NewLicense newLicense)
        {
            // Tạo id chứng từ mới:
            newLicense.License.LicenseId = Guid.NewGuid();

            // Thêm license mới:
            var sqlLicenseCommand = $"INSERT License (LicenseId, LicenseCode, UseDate, WriteUpDate, Description, Total, CreatedDate) " +
                $"VALUES(@LicenseId, @LicenseCode, @UseDate, @WriteUpDate, @Description, @Total, @CreatedDate)";
            var parameters = new DynamicParameters();
            parameters.Add("@LicenseId", newLicense.License.LicenseId);
            parameters.Add("@LicenseCode", newLicense.License.LicenseCode);
            parameters.Add("@UseDate", newLicense.License.UseDate);
            parameters.Add("@WriteUpDate", newLicense.License.WriteUpDate);
            parameters.Add("@Description", newLicense.License.Description);
            parameters.Add("@Total", newLicense.License.Total);
            parameters.Add("@CreatedDate", DateTime.Now);   
            // Thưc hiện câu lệnh sql:
            var licenseAdd = _sqlConnection.Execute(sqlLicenseCommand, param: parameters);

            // Thêm licensedetail mới:
            foreach (var detail in newLicense.licenseDetails)
            {
                detail.LicenseDetailId = Guid.NewGuid();
                var sqlDetailCommand = $"INSERT LicenseDetail (LicenseDetailId, LicenseId, FixedAssetId, DetailJson, CreatedDate) " +
                    $"VALUES(@LicenseDetailId, @LicenseId, @FixedAssetId, @DetailJson, @CreatedDate)";
                parameters.Add("@LicenseDetailId", detail.LicenseDetailId);
                parameters.Add("@LicenseId", newLicense.License.LicenseId);
                parameters.Add("@FixedAssetId", detail.FixedAssetId);
                parameters.Add("@DetailJson", detail.DetailJson);
                parameters.Add("@CreatedDate", DateTime.Now);

                // Thưc hiện câu lệnh sql:
                var detailDAdd = _sqlConnection.Execute(sqlDetailCommand, param: parameters);
            }
            return licenseAdd;

        }

        /// <summary>
        /// Lấy chi tiết chứng từ, kèm theo danh sách tài sản có chứng từ
        /// </summary>
        /// <param name="licenseId"></param>
        /// <returns></returns>
        public List<FixedAsset> GetLicenseDetail(Guid licenseId)
        {
            var sqlDetailCommand = $"SELECT * FROM FixedAsset INNER JOIN LicenseDetail ON FixedAsset.FixedAssetId = LicenseDetail.FixedAssetId WHERE LicenseDetail.LicenseId = @licenseId";
            var parameters = new DynamicParameters();
            parameters.Add("@LicenseId", licenseId);
            var getLicenseDetail = _sqlConnection.Query<FixedAsset>(sqlDetailCommand, param: parameters);
            return getLicenseDetail.ToList();
        }

        /// <summary>
        /// Xử lí sửa danh sách tài sản chứng từ:
        /// </summary>
        /// <param name="licenseId">Id của chứng từ cần sửa</param>
        /// <param name="licenseDetails">Danh sách tài sản mới</param>
        /// <returns>Số bản ghi đã sửa</returns>
        public int UpdatetLicenseDetail(Guid licenseId, List<LicenseDetail> licenseDetails)
        {
            //Tổng số bản ghi bị ảnh hưởng:
            var count = 0;
            // Lấy ra danh sách Id mới nhận:
            var newIdList = licenseDetails.Select(licenseDetail => licenseDetail.FixedAssetId).ToList();

            // Cập nhật thông tin chứng từ:
            // Cập nhật danh sách tài sản thuộc chứng từ:
            //1. Lấy danh sách bản ghi tài sản cũ:
            var sqlCommand = $"SELECT * FROM LicenseDetail WHERE LicenseId = @LicenseId";
            var parameters = new DynamicParameters();
            parameters.Add("@LicenseId", licenseId);
            var currentAssetlist = _sqlConnection.Query<LicenseDetail>(sqlCommand, param: parameters);
            // Lấy ra danh sách Id:
            var currentIdList = currentAssetlist.Select(licenseDetail => licenseDetail.FixedAssetId).ToList();

            //2. So sánh:
            //2.1 Nếu bản ghi đã tồn tại rồi => bỏ qua:
            var notChangeList = Enumerable.SequenceEqual(newIdList, currentIdList);

            //2.2 Nếu bản ghi chưa có => tạo bản ghi mới:
            var insertList = newIdList.Except(currentIdList).ToList();
            foreach (var asset in insertList)
            {
                var newGuid = Guid.NewGuid();
                var sqlInsert = $"INSERT LicenseDetail (LicenseDetailId, LicenseId, FixedAssetId) " +
                    $"VALUES(@LicenseDetailId, @LicenseId, @FixedAssetId)";
                parameters.Add("@LicenseDetailId", newGuid);
                parameters.Add("@FixedAssetId", asset);
                // Thưc hiện câu lệnh sql:
                var detailAdd = _sqlConnection.Execute(sqlInsert, param: parameters);
                count++;
            }

            //2.3 Nếu bản ghi không có trong list mới nhưng có trong list cũng => thì xóa đi:
            var deleteList = currentIdList.Except(newIdList).ToList();
            foreach (var asset in deleteList)
            {
                var sqlDelete = $"DELETE FROM LicenseDetail WHERE FixedAssetId = @FixedAssetId AND LicenseId = @LicenseId";
                parameters.Add("@FixedAssetId", asset);
                // Thưc hiện câu lệnh sql:
                var detailDelete = _sqlConnection.Execute(sqlDelete, param: parameters);
                count++;
            }
          

            return count;
        }

        ///
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

        public object FilterLicenseDetail(string? searchLicense, int pageIndex, int pageSize)
        {
            // Nếu trả về null thì gắn vào giá trị "":
            if (string.IsNullOrEmpty(searchLicense))
            {
                searchLicense = "";
            }
            // Tính giá trị tổng offset:
            var totalOffset = (pageIndex - 1) * pageSize;
            // Khởi tạo câu truy vấn: 
            var sqlQuery = "SELECT * FROM License WHERE (LicenseCode LIKE CONCAT('%',@searchLicense,'%') OR Description LIKE CONCAT('%',@searchLicense,'%')) ";
            var parameters = new DynamicParameters();
            parameters.Add("@searchLicense", searchLicense);
            var count = _sqlConnection.Query<License>(sqlQuery, param: parameters);

            // Phân trang: 
            sqlQuery += "ORDER BY CreatedDate DESC LIMIT @pageSize OFFSET @totalOffset";
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@totalOffset", totalOffset);
            var list = _sqlConnection.Query<License>(sqlQuery, param: parameters);

            // Trả về đối tượng:
            var res = new
            {
                List = (List<License>)list.ToList(),
                Count = (int)count.Count()
            };

            // Trả về dữ liệu dạng List:
            return res;
        }
    }
}
