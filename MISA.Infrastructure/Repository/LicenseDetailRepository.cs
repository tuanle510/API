using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class LicenseDetailRepository : BaseRepository<LicenseDetail>, ILicenseDetailRepository
    {
        public LicenseDetailRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<object> GetAssetByLicenseId(Guid licenseId)
        {
            var sqlCommand = $"SELECT * FROM FixedAsset JOIN LicenseDetail ON FixedAsset.FixedAssetId = LicenseDetail.FixedAssetId WHERE LicenseDetail.LicenseId = @licenseId";
            var parameters = new DynamicParameters();
            parameters.Add("@LicenseId", licenseId);
            var res = _sqlConnection.Query<object>(sqlCommand, param: parameters);
            return res.ToList();
        }

        public object GetLicenseDetai(Guid licenseDetailId)
        {
            var sqlCommand = "SELECT ld.LicenseDetailId, ld.LicenseId, ld.FixedAssetId, ld.DetailJson, fa.DepartmentName, fa.FixedAssetName FROM LicenseDetail ld JOIN FixedAsset fa ON ld.FixedAssetId = fa.FixedAssetId AND ld.LicenseDetailId = @licenseDetailId";
            var parameters = new DynamicParameters();
            parameters.Add("@licenseDetailId", licenseDetailId);
            var res = _sqlConnection.QueryFirstOrDefault<object>(sqlCommand, param: parameters);
            return res;
        }

        public int MultiInsert(Guid licenseId, List<LicenseDetail> licenseDetails)
        {
            var count = 0;
            foreach (var detail in licenseDetails)
            {
                detail.LicenseId = licenseId;
                var addDetail = Insert(detail);
                count += addDetail;
            }
            return count; // trả về số bản ghi bị ảnh hưởng
        }

        public int MultiUpdate(Guid licenseId, List<LicenseDetail> licenseDetails)
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
            //var notChangeList = Enumerable.SequenceEqual(newIdList, currentIdList);

            //2.2 Nếu bản ghi chưa có => tạo bản ghi mới:
            var insertList = newIdList.Except(currentIdList).ToList();

            foreach (var asset in insertList)
            {
                // gán lại detailJon từ mảng mới nhận về: 
                var detailJson = licenseDetails?.Find(item => item.FixedAssetId == asset)?.DetailJson;
                var newGuid = Guid.NewGuid();
                var sqlInsert = $"INSERT LicenseDetail (LicenseDetailId, LicenseId, FixedAssetId, DetailJson) " +
                    $"VALUES(@LicenseDetailId, @LicenseId, @FixedAssetId, @DetailJson)";
                parameters.Add("@LicenseDetailId", newGuid);
                parameters.Add("@FixedAssetId", asset);
                parameters.Add("@DetailJson", detailJson);
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

       
    }
}
