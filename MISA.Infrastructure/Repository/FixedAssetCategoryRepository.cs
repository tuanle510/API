using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Respositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class FixedAssetCategoryRepository: BaseRepository<FixedAssetCategory>, IFixedAssetCategoryRepository
    {
        public FixedAssetCategoryRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public int Delete(Guid entityId)
        {
            throw new NotImplementedException();
        }


        public int Insert(FixedAssetCategory entity)
        {
            throw new NotImplementedException();
        }

        public int Update(Guid id,FixedAssetCategory entity)
        {
            throw new NotImplementedException();
        }
    }
}
