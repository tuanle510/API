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
    public class LicenseRepository : BaseRepository<License>, ILicenseRepository
    {
        public LicenseRepository(IConfiguration configuration) : base(configuration)
        {

        }

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


            //SELECT * FROM FixedAsset fa INNER JOIN LicenseDetail ld ON fa.FixedAssetId = ld.FixedAssetId WHERE ld.LicenseId ="bda9c7b0-b8c0-472c-a7fb-ffc85d203170"
            var sqlDetailCommand = $"SELECT * FROM FixedAsset INNER JOIN LicenseDetail ON FixedAsset.FixedAssetId = LicenseDetail.FixedAssetId WHERE LicenseDetail.LicenseId = @licenseId";
            var parameters = new DynamicParameters();
            parameters.Add("@LicenseId", licenseId);
            var getLicenseDetail = _sqlConnection.Query<FixedAsset>(sqlDetailCommand, param: parameters);


            return getLicenseDetail.ToList();
        }
    }
}
