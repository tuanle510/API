using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Respositories
{
    public interface ILicenseDetailRepository : IBaseRepository<LicenseDetail>
    {
        /// <summary>
        /// Lấy danh sách tài sản theo mã chứng từ;
        /// </summary>
        /// <param name="licenseId"></param>
        /// <returns></returns>
        List<object> GetAssetByLicenseId(Guid licenseDetailId);


        /// <summary>
        /// Lấy dữ liệu license detail kèm theo bộ phận sử dụng
        /// </summary>
        /// <param name="licenseId">id cần lấy</param>
        /// <returns></returns>
        object GetLicenseDetai(Guid licenseId);

        /// <summary>
        /// Thêm nhiều bản ghi:
        /// </summary>
        /// <param name="licenseId"></param>
        /// <param name="licenseDetails"></param>
        /// <returns></returns>
        int MultiInsert(Guid licenseId, List<LicenseDetail> licenseDetails);

        /// <summary>
        /// Cập nhật danh sách tài sản trong chứng từ (Thêm, xóa nhiều)
        /// </summary>
        /// <param name="licenseId"></param>
        /// <param name="licenseDetails"></param>
        /// <returns></returns>
        int MultiUpdate(Guid licenseId, List<LicenseDetail> licenseDetails);
    }
}
