
using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface IFixedAssetService
    {
        /// <summary>
        /// Xử lí nghệp vụ khi thêm mới tài sản
        /// </summary>
        /// <param name="fixedAsset"></param>
        /// <returns>Số lượng bản ghi tài sản thêm mới vào Database</returns>
        /// CreatedBy: LTTUAN (10.05.2022)
        int InsertService(FixedAsset fixedAsset);
        int UpadteService(FixedAsset fixedAsset);
    }
}
