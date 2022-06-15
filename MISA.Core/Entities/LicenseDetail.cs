using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class LicenseDetail : BaseEntity
    {
        /// <summary>
        /// Id chứng từ
        /// Khóa chính
        /// </summary>
        public Guid LicenseDetailId { get; set; }

        /// <summary>
        /// Id chứng từ
        /// </summary>
        public Guid LicenseId { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        public Guid FixedAssetId { get; set; }

        /// <summary>
        /// Nguồn hình thành
        /// </summary>
        public string? DetailJson { get; set; }
    }
}
