using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Respositories
{
    public interface ILicenseRepository : IBaseRepository<License>
    {
        /// <summary>
        /// Thêm mới chứng từ
        /// </summary>
        /// <param name="newLicense"></param>
        /// <returns></returns>
        int AddLicenseDetail(NewLicense newLicense);

        /// <summary>
        /// Lấy danh sách tài sản có chứng từ theo id của chứng từ
        /// </summary>
        /// <param name="licenseId">ID chứng từ cần lấy danh sách</param>
        /// <returns> Danh sách tài sản có chứng từ đó </returns>
        List<FixedAsset> GetLicenseDetail(Guid licenseId);

        /// <summary>
        /// Sửa thông tin chứng từ, sửa danh sách tài sản thuộc chứng từ:
        /// </summary>
        /// <param name="licenseId">Id chứng từ cần sửa</param>
        /// <param name="newLicense">Danh sách tài sản thuộc chứng từ</param>
        /// <returns>Số lượng bản ghi được sửa</returns>
        int UpdatetLicense(Guid licenseId, NewLicense newLicense);
    }
}
