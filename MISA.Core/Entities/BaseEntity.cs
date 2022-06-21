using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class BaseEntity
    {
        /// <summary>
        /// Ngày tạo dữ liệu
        /// </summary>
        [CreateDate]
        public DateTime? CreatedDate { get; set; }

        //public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa dữ liệu
        /// </summary>
        [ModifiedDate]
        public DateTime? ModifiedDate { get; set; }

        //public string? ModifiedBy { get; set; }
    }
}
