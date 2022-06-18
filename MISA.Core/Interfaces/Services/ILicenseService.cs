using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface ILicenseService : IBaseService<License>
    {
        /// <summary>
        /// Xử lí sự kiện thêm mới chứng từ và danh sách tài sản có chứng từ:
        /// </summary>
        /// <param name="newLicense"></param>
        /// <returns></returns>
        public int InsertLicenseDetail(NewLicense newLicense);

        /// <summary>
        /// Xử lí sự kiện sửa chứng từ và danh sách tài sản có chứng từ:
        /// </summary>
        /// <param name="newLicense"></param>
        /// <returns></returns>
        public int UpdateLicenseDetail(Guid licenseId ,NewLicense newLicense);
    }
}
