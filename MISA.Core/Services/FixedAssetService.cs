using Dapper;
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
        public FixedAssetService(IFixedAssetRepository fixedAssetRepository) : base(fixedAssetRepository)
        {
            _fixedAssettRepository = fixedAssetRepository;
        }

        
        public object DeleteService(Guid[] fixedAssetIdList)
        {
            var isHasLicense = _fixedAssettRepository.CheckAssetHasLicense(fixedAssetIdList);
            if(isHasLicense == null)
            {
                var res = _fixedAssettRepository.DeleteMulti(fixedAssetIdList);
                return res;
            } 
            return isHasLicense;
        }
    }
}
