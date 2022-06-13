using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class License
    {
        /// <summary>
        /// Id chứng từ
        /// Khóa chính
        /// </summary>
        public Guid LicenseId { get; set; }

        /// <summary>
        /// Mã chứng từ
        /// </summary>
        public string LicenseCode { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        public DateTime UseDate { get; set; }

        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        public DateTime WriteUpDate { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Tổng nguyên giá
        /// </summary>
        public decimal Total { get; set; }
    }
}
