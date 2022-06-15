using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Respositories;
using MISA.Core.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web03.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LicensesController : MISABaseController<License>
    {
        ILicenseRepository _licenseRepository;
        ILicenseService _licenseService;
        public LicensesController(ILicenseService licenseService, ILicenseRepository licenseRepository) : base(licenseService, licenseRepository)
        {
            _licenseRepository = licenseRepository;
            _licenseService = licenseService;
        }

        [HttpPost("InsertNewLicense")]
        public IActionResult InsertNewLicense(NewLicense newLicense)

        {
            try
            {
                var res = _licenseRepository.AddLicenseDetail(newLicense);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("GetLicense/{id}")]
        public IActionResult GetLicense(Guid id)
        {
            try
            {
                var res = _licenseRepository.GetLicenseDetail(id);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
