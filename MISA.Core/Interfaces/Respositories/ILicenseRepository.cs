using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Respositories
{
    public interface ILicenseRepository : IBaseRepository<License>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchLicense"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        object FilterLicenseDetail(string? searchLicense, int pageIndex, int pageSize);

    }
}
