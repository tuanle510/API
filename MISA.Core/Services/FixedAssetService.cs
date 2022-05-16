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
    public class FixedAssetService : BaseService<FixedAsset>, IFixedAssetService
    {
        IFixedAssetRepository _fixedAssettRepository;
        public FixedAssetService(IFixedAssetRepository fixedAssetRepository):base(fixedAssetRepository)
        {
            _fixedAssettRepository = fixedAssetRepository;
        }

        public int InsertService(FixedAsset fixedAsset)
        {
            var mode = 1;
            // Validate dữ liệu
            // 1. Kiểm tra xem có trống hay không thì add vào list
            var validateErrorsMsg = new List<String>();
            if (string.IsNullOrEmpty(fixedAsset.FixedAssetCode))
            {
                validateErrorsMsg.Add("Mã tài sản không được trùng");
            }
            if (string.IsNullOrEmpty(fixedAsset.FixedAssetName))
            {
                validateErrorsMsg.Add("Tên tài sản không được trùng");
            }
            if (string.IsNullOrEmpty(fixedAsset.DepartmentCode))
            {
                validateErrorsMsg.Add("Mã bộ phận sử dụng không được để trống");
            }
            if (string.IsNullOrEmpty(fixedAsset.DepartmentName))
            {
                validateErrorsMsg.Add("Tên bộ phận sử dụng không được để trống");
            }
            if (string.IsNullOrEmpty(fixedAsset.FixedAssetCategoryCode))
            {
                validateErrorsMsg.Add("Mã loại tài sản không được để trống");
            }
            if (string.IsNullOrEmpty(fixedAsset.FixedAssetCategoryName))
            {
                validateErrorsMsg.Add("Tên loại tài sản không được để trống");
            }

            var isDuplicate = _fixedAssettRepository.CheckFixedAssetExist(fixedAsset.FixedAssetCode, fixedAsset.FixedAssetId, mode);
            if (isDuplicate == true)
            {
                validateErrorsMsg.Add("Mã tài sản đã tồn tại trong hệ thống");
            }

            // Kiểm tra trong errorList có lỗi nào không 
            if (validateErrorsMsg.Count > 0)
            {
                // Khởi tạo đối tượng (Khi có lỗi mới khỏi tạo đối tượng)
                var validateError = new ValidateError();
                validateError.UserMsg = "Đã có lỗi";
                validateError.Data = validateErrorsMsg;
                throw new MISAValidateException("Dữ liệu đầu vào không hợp lệ", validateErrorsMsg);
            }

            // Sau khi kiểm tra các thông tin đã hợp lệ thì thêm vào database
            var res = _fixedAssettRepository.Insert(fixedAsset);
            // Trả về kết quả cho client
            return res;
        }

        public int UpadteService(Guid id ,FixedAsset fixedAsset)
        {
            var mode = 2;
            // Validate dữ liệu
            // 1. Kiểm tra xem có trống hay không thì add vào list
            var validateErrorsMsg = new List<String>();
            if (string.IsNullOrEmpty(fixedAsset.FixedAssetCode))
            {
                validateErrorsMsg.Add("Mã tài sản không được trùng");
            }
            if (string.IsNullOrEmpty(fixedAsset.FixedAssetName))
            {
                validateErrorsMsg.Add("Tên tài sản không được trùng");
            }
            if (string.IsNullOrEmpty(fixedAsset.DepartmentCode))
            {
                validateErrorsMsg.Add("Mã bộ phận sử dụng không được để trống");
            }
            if (string.IsNullOrEmpty(fixedAsset.DepartmentName))
            {
                validateErrorsMsg.Add("Tên bộ phận sử dụng không được để trống");
            }
            if (string.IsNullOrEmpty(fixedAsset.FixedAssetCategoryCode))
            {
                validateErrorsMsg.Add("Mã loại tài sản không được để trống");
            }
            if (string.IsNullOrEmpty(fixedAsset.FixedAssetCategoryName))
            {
                validateErrorsMsg.Add("Tên loại tài sản không được để trống");
            }

            var isDuplicate = _fixedAssettRepository.CheckFixedAssetExist(fixedAsset.FixedAssetCode, id, mode);
            if (isDuplicate == true)
            {
                validateErrorsMsg.Add("Mã tài sản đã tồn tại trong hệ thống");
            }

            // Kiểm tra trong errorList có lỗi nào không 
            if (validateErrorsMsg.Count > 0)
            {
                // Khởi tạo đối tượng (Khi có lỗi mới khỏi tạo đối tượng)
                var validateError = new ValidateError();
                validateError.UserMsg = "Đã có lỗi";
                validateError.Data = validateErrorsMsg;
                throw new MISAValidateException("Dữ liệu đầu vào không hợp lệ", validateErrorsMsg);
            }

            // Sau khi kiểm tra các thông tin đã hợp lệ thì thêm vào database
            var res = _fixedAssettRepository.Update(id, fixedAsset);
            // Trả về kết quả cho client
            return res;
        }
    }
}
