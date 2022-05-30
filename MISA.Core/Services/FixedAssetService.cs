 using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Respositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class FixedAssetService : BaseService<FixedAsset>, IFixedAssetService
    {
        IFixedAssetRepository _fixedAssettRepository;
        public FixedAssetService(IFixedAssetRepository fixedAssetRepository):base(fixedAssetRepository)
        {
            _fixedAssettRepository = fixedAssetRepository;
        }
    }
}
