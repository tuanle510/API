using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Respositories;
using MISA.Core.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web03.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LicenseDetailController : MISABaseController<LicenseDetail>
    {
        ILicenseDetailRepository _licensedetailRepository;
        public LicenseDetailController(IBaseService<LicenseDetail> baseService, IBaseRepository<LicenseDetail> baseRepository, ILicenseDetailRepository licenseDetailRepository) : base(baseService, baseRepository)
        {
            _licensedetailRepository = licenseDetailRepository;
        }

        [HttpGet("GetLicenseDetail/{licenseDetailId}")]
        public IActionResult GetLicenseDetail(Guid licenseDetailId)
        {
            try
            {
                var res = _licensedetailRepository.GetLicenseDetai(licenseDetailId);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
