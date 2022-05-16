using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Respositories
{
    public interface IFixedAssetRepository: IBaseRepository<FixedAsset>
    {
        FixedAsset GetById(Guid id);

        string GetNewFixedAssetCode();

        List<FixedAsset> GetPaging(int pageIndex, int pageSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fixedAssetCode"></param>
        /// <returns>true - đã tồn tại, false - chưa tồn tại</returns>
        bool CheckFixedAssetExist(string fixedAssetCode, Guid id, int mode);

    }
}
