using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface IBaseService<T>
    {
        /// <summary>
        /// Xử lí nghệp vụ khi thêm mới đối tượng
        /// </summary>
        /// <param></param>
        /// <returns>Số lượng bản ghi thêm mới vào Database</returns>
        /// CreatedBy: LTTUAN (10.05.2022)
        int InsertService(T entity);
    }
}
