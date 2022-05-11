using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Respositories
{
    public interface IFixedAssetRepository
    {
        List<FixedAsset> Get();

        int Insert(FixedAsset fixedAsset);

        int Update(FixedAsset fixedAsset);

        int Delete(Guid  fixedAssetId);

        List<FixedAsset> GetPaging(int pageIndex, int pageSize);

        string GetNewFixedAssetCode();

        bool CheckFixedAssetExist(string fixedAssetCode);

    }
}
