using MISA.Core.Entities;
using MISA.Core.Interfaces.Respositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class FixedAssetService : IFixedAssetService
    {
        IFixedAssetRepository _fixedAssettRepository;
        public FixedAssetService(IFixedAssetRepository fixedAssetRepository)
        {
            _fixedAssettRepository = fixedAssetRepository;
        }

        public int InsertService(FixedAsset fixedAsset)
        {
            // Validate dữ liệu
            // 1. Kiểm tra xem có trống hay không thì add vào list
            //var validateErrorsMsg = new List<String>();
            //if (string.IsNullOrEmpty(fixedAsset.fixed_asset_code))
            //{
            //    validateErrorsMsg.Add("Mã tài sản không được trùng");
            //}
            //if (string.IsNullOrEmpty(fixedAsset.fixed_asset_name))
            //{
            //    validateErrorsMsg.Add("Tên tài sản không được trùng");
            //}
            //if (string.IsNullOrEmpty(fixedAsset.department_code))
            //{
            //    validateErrorsMsg.Add("Mã bộ phận sử dụng không được để trống");
            //}
            //if (string.IsNullOrEmpty(fixedAsset.department_name))
            //{
            //    validateErrorsMsg.Add("Tên bộ phận sử dụng không được để trống");
            //}
            //if (string.IsNullOrEmpty(fixedAsset.fixed_asset_category_code))
            //{
            //    validateErrorsMsg.Add("Mã loại tài sản không được để trống");
            //}
            //if (string.IsNullOrEmpty(fixedAsset.fixed_asset_category_name))
            //{
            //    validateErrorsMsg.Add("Tên loại tài sản không được để trống");
            //}

            // 1.1 Kiểm tra trong errorList có lỗi nào không 
            //if (validateErrorsMsg.Count > 0)
            //{
            //    // Khởi tạo đối tượng (Khi có lỗi mới khỏi tạo đối tượng)
            //    var validateError = new ValidateError();
            //    validateError.UserMsg ="Trùng";
            //    validateError.Data = validateErrorsMsg;
            //    return StatusCode(400, validateError);
            //}
            //// 2. Kiểm tra xem mã đã tồn tại hay chưa?
            //var sqlQueryCheckDuplicateCode = $"SELECT fixed_asset_code FROM fixed_asset WHERE fixed_asset_code=@fixedAssetCode";
            //var employeeCodeDuplicate = sqlConnection.QueryFirstOrDefault<string>(sqlQueryCheckDuplicateCode, param: new { fixedAssetCode = fixedAsset.fixed_asset_code }); 
            ////Nếu có 1 tham số chuyền vào thì dùng luôn param new                                                                                                                                     
            ////var parametersDup = new DynamicParameters();                                                                                                        
            ////parametersDup.Add("@fixedAssetCode", fixedAsset.fixed_asset_code);                                                                         
            ////var employeeCodeDuplicate = sqlConnection.QueryFirstOrDefault<string>(sqlQueryCheckDuplicateCode, param: parametersDup);
            //if (employeeCodeDuplicate != null)
            //{
            //    var validateError = new ValidateError();
            //    validateError.UserMsg = "Mã tài sản đã tổn tại trong hệ thống, vui lòng kiểm tra lại";
            //    return StatusCode(400, validateError);
            //}
            // Sau khi kiểm tra các thông tin đã hợp lệ thì thêm vào database
            var res = _fixedAssettRepository.Insert(fixedAsset);
            // Trả về kết quả cho client
            return res;
        }

        public int UpadteService(FixedAsset fixedAsset)
        {
            // Xử lí về nghiệp vụ
            // 1. Kiểm tra đầu vào có hay chưa 

            // 2. Kiểm tra mã tài sản có trùng hay không? => trùng thì báo lỗi

            // Sau khi kiểm tra các thông tin đã hợp lệ thì cập nhật vào database

            // Trả về kết quả cho client
            throw new NotImplementedException();
        }
    }
}
