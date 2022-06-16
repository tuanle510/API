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

        /// <summary>
        /// Xử lí tạo mới chứng từ (Thông tin chứng từ, Danh sách tài sản chứng từ)
        /// </summary>
        /// <param name="newLicense"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Xử lí lấy chứng từ theo ID (Cả thông tin chứng từ và danh sách tài sản trong chứng từ)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetLicense/{id}")]
        public IActionResult GetLicense(Guid id)
        {
            try
            {
                var getLicense = _licenseRepository.GetById(id);
                var getLicenseDetail = _licenseRepository.GetLicenseDetail(id);

                var licenseDetail = new
                {
                    License = getLicense,
                    FixedAssetList = getLicenseDetail
                };
                return StatusCode(200, licenseDetail);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut("UpdateLicense/{licenseId}")]
        public IActionResult UpDateLicense(Guid licenseId, NewLicense newLicense)
        {
            try
            {
                var res = _licenseRepository.UpdatetLicense(licenseId, newLicense);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
