using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Respositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // So sánh ngày bắt đầu sử dụng và ngày ghi tăng:
            var writeUp = license.WriteUpDate;
            var useDate = license.UseDate;

            // Kiểm tra nếu danh sách tài sản trống: => trả về cảnh báo
            if (licenseDetails.Count == 0)
            {
                ValidateErrorsMsg.Add("Chọn ít nhất 1 tài sản.");
                // Nếu có lỗi khởi tạo đối tượng (Khi có lỗi mới khỏi tạo đối tượng)
                var validateError = new ValidateError();
                validateError.UserMsg = "Đã có lỗi";
                validateError.Data = ValidateErrorsMsg;
                throw new MISAValidateException("Dữ liệu đầu vào không hợp lệ", ValidateErrorsMsg);
            }
            // Nếu danh sách tài sản đã có dữ liệu: => thực hiện thêm
            else
            {
                // Tạo Id mới
                license.LicenseId = Guid.NewGuid();
                // Thêm mới bảng License:
                var insertLicense = InsertService(license);
                // Thêm mới bảng LicenseDetail:
                var insertLicenseDetail = _licenseDetailRepository.MultiInsert(license.LicenseId, licenseDetails);
                return insertLicense + insertLicenseDetail;
            }
        }

        public int UpdateLicenseDetail(Guid licenseId, NewLicense newLicense)
        {
            // Tách đối tượng: 
            var license = newLicense.License;
            var licenseDetails = newLicense.LicenseDetails;
            // Kiểm tra nếu danh sách tài sản trống: => trả về cảnh báo
            if (licenseDetails.Count == 0)
            {
                ValidateErrorsMsg.Add("Chọn ít nhất 1 tài sản.");
                // Nếu có lỗi khởi tạo đối tượng (Khi có lỗi mới khỏi tạo đối tượng)
                var validateError = new ValidateError();
                validateError.UserMsg = "Đã có lỗi";
                validateError.Data = ValidateErrorsMsg;
                throw new MISAValidateException("Dữ liệu đầu vào không hợp lệ", ValidateErrorsMsg);
            }
            // Nếu danh sách tài sản đã có dữ liệu: => thực hiện sửa
            else
            {
                // Sửa thông tin chứng từ:
                var updateLicense = UpdateService(licenseId, license);
                // Sửa danh sách tài sản có chứng từ:
                var updateLicenseDetail = _licenseDetailRepository.MultiUpdate(licenseId, licenseDetails);
                return updateLicense + updateLicenseDetail;
            }
        }
    }
}
