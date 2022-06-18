using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class License : BaseEntity
    {
        /// <summary>
        /// Id chứng từ
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        [IsNotNullOrEmpty]
        public Guid LicenseId { get; set; }

        /// <summary>
        /// Mã chứng từ
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Mã chứng từ")]
        [MaxLength(50)]
        public string LicenseCode { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Ngày bắt đầu sử dụng")]
        [MaxLength(255)]
        public DateTime UseDate { get; set; }

        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        [IsNotNullOrEmpty]
        [FriendlyName("Ngày ghi tăng")]
        [MaxLength(255)]
        public DateTime WriteUpDate { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        [FriendlyName("Ghi chú")]
        public string? Description { get; set; }

        /// <summary>
        /// Tổng nguyên giá
        /// </summary>
        [FriendlyName("Tổng nguyên giá")]
        public decimal Total { get; set; }
    }
}
