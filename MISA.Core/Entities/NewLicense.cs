using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class NewLicense
    {
        public License License { get; set; }
        public List<LicenseDetail> LicenseDetails { get; set; }
    }
}
