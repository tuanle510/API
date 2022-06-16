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
            var sqlLicenseCommand = $"INSERT License (LicenseId, LicenseCode, UseDate, WriteUpDate, Description, Total) " +
                $"VALUES(@LicenseId, @LicenseCode, @UseDate, @WriteUpDate, @Description, @Total)";
            var parameters = new DynamicParameters();
            parameters.Add("@LicenseId", newLicense.License.LicenseId);
            parameters.Add("@LicenseCode", newLicense.License.LicenseCode);
            parameters.Add("@UseDate", newLicense.License.UseDate);
            parameters.Add("@WriteUpDate", newLicense.License.WriteUpDate);
            parameters.Add("@Description", newLicense.License.Description);
            parameters.Add("@Total", newLicense.License.Total);
            // Thưc hiện câu lệnh sql:
            var licenseAdd = _sqlConnection.Execute(sqlLicenseCommand, param: parameters);

            // Thêm licensedetail mới:
            foreach (var detail in newLicense.licenseDetails)
            {
                detail.LicenseDetailId = Guid.NewGuid();
                var sqlDetailCommand = $"INSERT LicenseDetail (LicenseDetailId, LicenseId, FixedAssetId, DetailJson) " +
                    $"VALUES(@LicenseDetailId, @LicenseId, @FixedAssetId, @DetailJson)";
                parameters.Add("@LicenseDetailId", detail.LicenseDetailId);
                parameters.Add("@LicenseId", newLicense.License.LicenseId);
                parameters.Add("@FixedAssetId", detail.FixedAssetId);
                parameters.Add("@DetailJson", detail.DetailJson);
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
        /// 
        /// </summary>
        /// <param name="licenseId"> Id của chứng từ cần sửa </param>
        /// <param name="newLicense"> Thông tin đối tượng cần sửa (gồm thông tin chứng từ, ) </param>
        /// <returns></returns>
        public int UpdatetLicense(Guid licenseId, NewLicense newLicense)
        {
            //TODO: cập nhật thông tin chứng từ:
            // Lấy thông tin chứng từ mới:
            var license = newLicense.License;
            // Lấy danh sách tài sản thuộc chứng từ mới:
            var licenseDetails = newLicense.licenseDetails;
            // Lấy ra danh sách Id:
            var newIdList = licenseDetails.Select(licenseDetail => licenseDetail.FixedAssetId).ToList();

            // Cập nhật thông tin chứng từ:
            // Cập nhật danh sách tài sản thuộc chứng từ:
            //1. Lấy danh sách bản ghi tài sản cũ:
            var sqlCommand = $"SELECT * FROM LicenseDetail";
            var currentAssetlist = _sqlConnection.Query<LicenseDetail>(sqlCommand);
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
                var parameters = new DynamicParameters();

                parameters.Add("@LicenseDetailId", newGuid);
                parameters.Add("@LicenseId", newLicense.License.LicenseId);
                parameters.Add("@FixedAssetId", asset);
                // Thưc hiện câu lệnh sql:
                var detailDAdd = _sqlConnection.Execute(sqlInsert, param: parameters);
            }

            //2.3 Nếu bản ghi không có trong list mới nhưng có trong list cũng => thì xóa đi:
            var deleteList = currentIdList.Except(newIdList).ToList();
            foreach (var asset in deleteList)
            {
                var sqlDelete = $"DELETE FROM LicenseDetail WHERE FixedAssetId = @FixedAssetId AND LicenseId = @LicenseId";
                var parameters = new DynamicParameters();

                parameters.Add("@LicenseId", asset);
                parameters.Add("@FixedAssetId", newLicense.License.LicenseId);
                // Thưc hiện câu lệnh sql:
                var detailDAdd = _sqlConnection.Execute(sqlDelete, param: parameters);
            }


            return currentAssetlist.Count();
        }
    }
}
