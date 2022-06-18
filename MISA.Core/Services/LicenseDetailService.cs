using MISA.Core.Entities;
using MISA.Core.Interfaces.Respositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class LicenseDetailService : BaseService<LicenseDetail>, ILicenseDetailService
    {
        ILicenseDetailRepository _licenseDetailRepository;
        public LicenseDetailService(ILicenseDetailRepository licenseDetailRepository) : base(licenseDetailRepository)
        {
            _licenseDetailRepository = licenseDetailRepository;
        }
    }
}
