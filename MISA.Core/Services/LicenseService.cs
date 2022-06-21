using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Respositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MISA.Core.Services
{
    public class LicenseService : BaseService<License>, ILicenseService
    {
        ILicenseRepository _licenseRepository;
        ILicenseDetailRepository _licenseDetailRepository;
        public LicenseService(ILicenseRepository licenseRepository, ILicenseDetailRepository licenseDetailRepository) : base(licenseRepository)
        {
            _licenseRepository = licenseRepository;
            _licenseDetailRepository = licenseDetailRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="license"></param>
        /// <returns></returns>
        protected override bool ValidateObjectCustom(License license)
        {
            var writeUp = license.WriteUpDate;
            var useDate = license.UseDate;
            var compareDate = DateTime.Compare(useDate, writeUp);
            if (compareDate > 0)
            {
                ValidateErrorsMsg.Add("Ngày ghi tăng không được sớm hơn ngày sử dụng");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Xử lí sự kiện thêm mới chứng từ và danh sách tài sản có chứng từ:
        /// </summary>
        /// <param name="newLicense"></param>
        /// <returns></returns>
        /// <exception cref="MISAValidateException"></exception>
        public int InsertLicenseDetail(NewLicense newLicense)
        {
            // Tách đối tượng: 
            var license = newLicense.License;
            var licenseDetails = newLicense.LicenseDetails;

            // Tạo Id mới
            license.LicenseId = Guid.NewGuid();
            // Thêm mới bảng License:
            var insertLicense = InsertService(license);
            // Thêm mới bảng LicenseDetail:
            var insertLicenseDetail = _licenseDetailRepository.MultiInsert(license.LicenseId, licenseDetails);
            return insertLicense + insertLicenseDetail;
        }

        /// <summary>
        /// Xử lí 
        /// </summary>
        /// <param name="licenseId"></param>
        /// <param name="newLicense"></param>
        /// <returns></returns>
        public int UpdateLicenseDetail(Guid licenseId, NewLicense newLicense)
        {
            // Tách đối tượng: 
            var license = newLicense.License;
            var licenseDetails = newLicense.LicenseDetails;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    // Sửa thông tin chứng từ:
                    var updateLicense = UpdateService(licenseId, license);
                    // Sửa danh sách tài sản có chứng từ:
                    var updateLicenseDetail = _licenseDetailRepository.MultiUpdate(licenseId, licenseDetails);

                    var result = updateLicense + updateLicenseDetail;
                    scope.Complete();

                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }
    }
}
