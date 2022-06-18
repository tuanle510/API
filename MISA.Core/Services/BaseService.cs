using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Respositories;
using MISA.Core.Interfaces.Services;
using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        IBaseRepository<T> _baseRepository;
        protected List<String> ValidateErrorsMsg;
        string _tableName;
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
            ValidateErrorsMsg = new List<String>();
            _tableName = typeof(T).Name;
        }

        /// <summary>
        /// Xử lí nghiệp vụ chung khi thêm mới đối tượng
        /// </summary>
        /// <param name="entity"> Đối tượng thêm mới </param>
        /// <returns> Số lượng bản ghi </returns>
        public int InsertService(T entity)
        {
            // Xử lí validate chung:
            var isValid = ValidateObject(entity);

            // Check mã trùng
            // Lấy giá trị code của entity:
            var entityCodeToCheck = typeof(T).GetProperty($"{ _tableName}Code").GetValue(entity, null).ToString();
            // Lấy giá trị friendlyName:
            var entityFriendlyName = "";
            // Kiểm tra xem có friendlyName không:
            var isFriendlyName = typeof(T).GetProperty($"{ _tableName}Code").IsDefined(typeof(FriendlyName), true);
            // Nếu có thì lấy ra:
            if (isFriendlyName)
            {
                entityFriendlyName = (typeof(T).GetProperty($"{ _tableName}Code").GetCustomAttributes(typeof(FriendlyName), true)[0] as FriendlyName).Name;
            }
            // Kiểm tra code có bị trùng không:
            if (_baseRepository.CheckCodeExist(entityCodeToCheck, Guid.Empty) == true)
            {
                ValidateErrorsMsg.Add(string.Format(Resources.ResourceVN.ErrorValidate_PropertyDuplicate, entityFriendlyName));
            }

            // Thực hiện thêm mới dữ liệu: 
            if (isValid == true && (ValidateErrorsMsg == null || ValidateErrorsMsg.Count() == 0))
                // Nếu không có lỗi thì thực hiện insert
                return _baseRepository.Insert(entity);
            else
            {
                // Nếu có lỗi khởi tạo đối tượng (Khi có lỗi mới khỏi tạo đối tượng)
                var validateError = new ValidateError();
                validateError.UserMsg = "Đã có lỗi";
                validateError.Data = ValidateErrorsMsg;
                throw new MISAValidateException("Dữ liệu đầu vào không hợp lệ", ValidateErrorsMsg);
            }
        }

        /// <summary>
        /// Xử lí nghiệp vụ khi sửa dối tượng
        /// </summary>
        /// <param name="entityId"> Id của đối tượng </param>
        /// <param name="entity"> đối tượng đã sửa </param>
        /// <returns></returns>
        public int UpdateService(Guid entityId, T entity)
        {
            // Xử lí validate chung:
            var isValid = ValidateObject(entity);

            // Check mã trùng
            // Lấy giá trị code của entity:
            var entityCodeToCheck = typeof(T).GetProperty($"{ _tableName}Code").GetValue(entity, null).ToString();

            // lấy FiendLy Name của entity:
            var entityFriendlyName = "";
            // Kiểm tra xem có friendlyName không:
            var isFriendlyName = typeof(T).GetProperty($"{ _tableName}Code").IsDefined(typeof(FriendlyName), true);
            // Nếu có thì lấy ra:
            if (isFriendlyName)
            {
                entityFriendlyName = (typeof(T).GetProperty($"{ _tableName}Code").GetCustomAttributes(typeof(FriendlyName), true)[0] as FriendlyName).Name;
            }
            // Kiểm tra xem code có trùng không
            if (_baseRepository.CheckCodeExist(entityCodeToCheck, entityId) == true)
            {
                ValidateErrorsMsg.Add(string.Format(Resources.ResourceVN.ErrorValidate_PropertyDuplicate, entityFriendlyName));
            }

            // Thực hiện thêm mới dữ liệu: 
            if (isValid == true && (ValidateErrorsMsg == null || ValidateErrorsMsg.Count() == 0))
                // Nếu không có lỗi thì thực hiện insert
                return _baseRepository.Update(entityId, entity);
            else
            {
                // Nếu có lỗi khởi tạo đối tượng (Khi có lỗi mới khỏi tạo đối tượng)
                var validateError = new ValidateError();
                validateError.UserMsg = "Đã có lỗi";
                validateError.Data = ValidateErrorsMsg;
                throw new MISAValidateException(Resources.ResourceVN.ErrorValidate_VN, ValidateErrorsMsg);
            }
        }

        /// <summary>
        /// Validate chung
        /// </summary>
        /// <param name="entity"> Đối tượng cần validate </param>
        /// <returns> true - nếu dữ liệu hợp lê, false - dữ liệu không hợp lệ </returns>
        /// CreatedBy: LTTUAN (17.05.2022) 
        private bool ValidateObject(T entity)
        {
            var isValidate = true;

            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                // Tên của prop:
                var propName = prop.Name;
                var propFriendlyName = propName;
                // Giá trị của prop:
                var propValue = prop.GetValue(entity);
                // Kiểu dữ liệu của prop:
                var propType = prop.PropertyType;

                // Kiểm tra xem có friendly name hay không
                var isFriendlyName = prop.IsDefined(typeof(FriendlyName), true);
                if (isFriendlyName)
                {
                    propFriendlyName = (prop.GetCustomAttributes(typeof(FriendlyName), true)[0] as FriendlyName).Name;
                }

                // 1. Thông tin bắt buộc nhập
                var isNotNullOrEmpty = prop.IsDefined(typeof(IsNotNullOrEmpty), true);
                if (isNotNullOrEmpty == true && (propValue == null || propValue.ToString() == ""))
                {
                    isValidate = false;
                    ValidateErrorsMsg.Add(string.Format(Resources.ResourceVN.ErrorValidate_ProperyNottNull, propFriendlyName));
                }

                // 2. Thông tin giới hạn về độ dài
                var isMaxLength = prop.IsDefined(typeof(MaxLength), true);
                if (isMaxLength)
                {
                    var maxLength = (prop.GetCustomAttributes(typeof(MaxLength), true)[0] as MaxLength).Length;
                    if (propValue.ToString().Length > maxLength)
                    {
                        isValidate = false;
                        ValidateErrorsMsg.Add(string.Format(Resources.ResourceVN.ErrorValidate_ProperyMaxLength, propFriendlyName, maxLength));
                    }
                }
            }
            return isValidate;
            ValidateObjectCustom(entity);
        }

        /// <summary>
        /// Thực hiện validate dữ liệu cho từng đối tượng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>List string các lỗi validate</returns>
        protected virtual List<String> ValidateObjectCustom(T entity)
        {
            return null;
        }
    }
}
