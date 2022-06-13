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
    public class LicenseService : BaseService<License>, ILicenseService
    {
        ILicenseRepository  _licenseRepository;
        public LicenseService(ILicenseRepository licenseRepository):base(licenseRepository)
        {
            _licenseRepository = licenseRepository;
        }
    }
}
