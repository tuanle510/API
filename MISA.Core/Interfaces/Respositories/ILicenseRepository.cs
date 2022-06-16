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

        List<FixedAsset> GetLicenseDetail(Guid licenseId);
    }
}
