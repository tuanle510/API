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

        //[HttpGet("Filter")]

        /// <summary>
        /// Xử lí tạo mới chứng từ (Thông tin chứng từ, Danh sách tài sản chứng từ)
        /// </summary>
        /// <param name="newLicense"></param>
        /// <returns></returns>
        [HttpPost("InsertLicense")]
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
        [HttpGet("GetLicense/{licenseId}")]
        public IActionResult GetLicense(Guid licenseId)
        {
            try
            {
                var getLicense = _licenseRepository.GetById(licenseId);
                var getLicenseDetail = _licenseRepository.GetLicenseDetail(licenseId);

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

        /// <summary>
        /// Xử lí sửa thông tin chứng từ và danh sách tài sản trong chứng từ
        /// </summary>
        /// <param name="licenseId">Id chứng từ cần sửa</param>
        /// <param name="newLicense">Đối tượng đã sửa</param>
        /// <returns>Số bản ghi đã được sửa</returns>
        [HttpPut("UpdateLicense/{licenseId}")]
        public IActionResult UpDateLicense(Guid licenseId, NewLicense newLicense)
        {
            try
            {
                var license = _licenseRepository.Update(licenseId, newLicense.License);
                var licenseDetails = _licenseRepository.UpdatetLicenseDetail(licenseId, newLicense.licenseDetails);
                return StatusCode(200, licenseDetails + license);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("FilterLicense")]
        public   IActionResult FilterLicense(string? searchLicense = "", int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                var res = _licenseRepository.FilterLicenseDetail(searchLicense, pageIndex, pageSize);
                return StatusCode(200, res);

            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
    }
}
